﻿using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace HuyaFX.UI.Wnd
{
    public partial class SWWindow : Window
    {
        private HwndSource _hwndSource;

        private bool isMouseButtonDown;
        private bool isManualDrag;
        private System.Windows.Point mouseDownPosition;
        private System.Windows.Point positionBeforeDrag;
        private System.Windows.Point previousScreenBounds;

        public Grid WindowRoot { get; private set; }
        public Grid LayoutRoot { get; private set; }
        public System.Windows.Controls.Button MinimizeButton { get; private set; }
        public System.Windows.Controls.Button MaximizeButton { get; private set; }
        public System.Windows.Controls.Button RestoreButton { get; private set; }
        public System.Windows.Controls.Button CloseButton { get; private set; }
        public Grid HeaderBar { get; private set; }
        public Grid HeaderTitleBar { get; set; }

        public double HeightBeforeMaximize { get; private set; }
        public double WidthBeforeMaximize { get; private set; }
        public WindowState PreviousState { get; private set; }

        static SWWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SWWindow),
                new FrameworkPropertyMetadata(typeof(SWWindow)));
        }

        public SWWindow()
        {
            double currentDPIScaleFactor = DPIHelper.GetCurrentDPIScaleFactor();
            Screen screen = Screen.FromHandle((new WindowInteropHelper(this)).Handle);
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            base.StateChanged += new EventHandler(this.OnStateChanged);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            Rectangle workingArea = screen.WorkingArea;
            base.MaxHeight = (double)(workingArea.Height + 16) / currentDPIScaleFactor;
            SystemEvents.DisplaySettingsChanged += new EventHandler(this.SystemEvents_DisplaySettingsChanged);
            this.AddHandler(Window.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnMouseButtonUp), true);
            this.AddHandler(Window.MouseMoveEvent, new System.Windows.Input.MouseEventHandler(this.OnMouseMove));
        }

        public T GetRequiredTemplateChild<T>(string childName) where T : DependencyObject
        {
            return (T)base.GetTemplateChild(childName);
        }

        public override void OnApplyTemplate()
        {
            this.WindowRoot = this.GetRequiredTemplateChild<Grid>("WindowRoot");
            this.LayoutRoot = this.GetRequiredTemplateChild<Grid>("LayoutRoot");
            this.MinimizeButton = this.GetRequiredTemplateChild<System.Windows.Controls.Button>("MinimizeButton");
            this.MaximizeButton = this.GetRequiredTemplateChild<System.Windows.Controls.Button>("MaximizeButton");
            this.RestoreButton = this.GetRequiredTemplateChild<System.Windows.Controls.Button>("RestoreButton");
            this.CloseButton = this.GetRequiredTemplateChild<System.Windows.Controls.Button>("CloseButton");
            this.HeaderBar = this.GetRequiredTemplateChild<Grid>("PART_HeaderBar");
            this.HeaderTitleBar = this.GetRequiredTemplateChild<Grid>("WndTitleLayout");

            if (this.LayoutRoot != null && this.WindowState == WindowState.Maximized)
            {
                this.LayoutRoot.Margin = GetDefaultMarginForDpi();
            }

            if (this.CloseButton != null)
            {
                this.CloseButton.Click += CloseButton_Click;
            }

            if (this.MinimizeButton != null)
            {
                this.MinimizeButton.Click += MinimizeButton_Click;
            }

            if (this.RestoreButton != null)
            {
                this.RestoreButton.Click += RestoreButton_Click;
            }

            if (this.MaximizeButton != null)
            {
                this.MaximizeButton.Click += MaximizeButton_Click;
            }

            if (this.HeaderBar != null)
            {
                this.HeaderBar.AddHandler(Grid.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnHeaderBarMouseLeftButtonDown));
            }

            base.OnApplyTemplate();
        }

        protected override void OnInitialized(EventArgs e)
        {
            try
            {
                SourceInitialized += OnSourceInitialized;
                base.OnInitialized(e);
            }
            catch { }
        }

        protected virtual void OnHeaderBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isManualDrag)
            {
                return;
            }

            System.Windows.Point position = e.GetPosition(this);
            // 我们产品暂时没有点击标题logo弹出系统菜单的需求
            //int headerBarHeight = 36;
            //int leftmostClickableOffset = 50;

            //if (position.X - this.LayoutRoot.Margin.Left <= leftmostClickableOffset && position.Y <= headerBarHeight)
            //{
            //    if (e.ClickCount != 2)
            //    {
            //        this.OpenSystemContextMenu(e);
            //    }
            //    else
            //    {
            //        base.Close();
            //    }
            //    e.Handled = true;
            //    return;
            //}

            if (e.ClickCount == 2 && base.ResizeMode == ResizeMode.CanResize)
            {
                this.ToggleWindowState();
                return;
            }

            if (base.WindowState == WindowState.Maximized)
            {
                this.isMouseButtonDown = true;
                this.mouseDownPosition = position;
            }
            else
            {
                try
                {
                    this.positionBeforeDrag = new System.Windows.Point(base.Left, base.Top);
                    this.DragMove();
                }
                catch
                {
                }
            }
        }

        protected void ToggleWindowState()
        {
            if (base.WindowState != WindowState.Maximized)
            {
                base.WindowState = WindowState.Maximized;
            }
            else
            {
                base.WindowState = WindowState.Normal;
            }
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleWindowState();
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleWindowState();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            OnMinimizeButtonClicked();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OnCloseButtonClicked();
        }

        protected virtual void OnCloseButtonClicked()
        {

        }

        protected virtual void OnMinimizeButtonClicked()
        {
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }

        private void SetMaximizeButtonsVisibility(Visibility maximizeButtonVisibility, Visibility reverseMaximizeButtonVisiility)
        {
            if (this.MaximizeButton != null)
            {
                this.MaximizeButton.Visibility = maximizeButtonVisibility;
            }
            if (this.RestoreButton != null)
            {
                this.RestoreButton.Visibility = reverseMaximizeButtonVisiility;
            }
        }

        private void OpenSystemContextMenu(MouseButtonEventArgs e)
        {
            System.Windows.Point position = e.GetPosition(this);
            System.Windows.Point screen = this.PointToScreen(position);
            int num = 36;
            if (position.Y < (double)num)
            {
                IntPtr handle = (new WindowInteropHelper(this)).Handle;
                IntPtr systemMenu = NativeUtils.GetSystemMenu(handle, false);
                if (base.WindowState != WindowState.Maximized)
                {
                    NativeUtils.EnableMenuItem(systemMenu, 61488, 0);
                }
                else
                {
                    NativeUtils.EnableMenuItem(systemMenu, 61488, 1);
                }
                int num1 = NativeUtils.TrackPopupMenuEx(systemMenu, NativeUtils.TPM_LEFTALIGN | NativeUtils.TPM_RETURNCMD, Convert.ToInt32(screen.X + 2), Convert.ToInt32(screen.Y + 2), handle, IntPtr.Zero);
                if (num1 == 0)
                {
                    return;
                }

                NativeUtils.PostMessage(handle, 274, new IntPtr(num1), IntPtr.Zero);
            }
        }

        //  关闭、最小化按键区域背景颜色
        public static readonly DependencyProperty WindowControlsGridBackgroundProperty =
            DependencyProperty.Register("WindowControlsGridBackground", typeof(System.Windows.Media.SolidColorBrush), typeof(SWWindow));
        public System.Windows.Media.SolidColorBrush WindowControlsGridBackground
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(WindowControlsGridBackgroundProperty); }
            set { SetValue(WindowControlsGridBackgroundProperty, value); }
        }

        //  标题栏高度
        public static readonly DependencyProperty TitleBarHeightProperty =
            DependencyProperty.Register("TitleBarHeight", typeof(double), typeof(SWWindow));
        public double TitleBarHeight
        {
            get { return (double)GetValue(TitleBarHeightProperty); }
            set { SetValue(TitleBarHeightProperty, value); }
        }
    }
}
