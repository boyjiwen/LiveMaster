using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HuyaFX.UI.Controls.Buttons
{
    public abstract class FXIconTextButtonBase : Button
    {

        #region Icon

        /// <summary>
        /// Normal 状态图标
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(FXIconTextButtonBase));
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Hover 状态图标
        /// </summary>
        public static readonly DependencyProperty HoverIconProperty =
           DependencyProperty.Register("HoverIcon", typeof(ImageSource), typeof(FXIconTextButtonBase));
        public ImageSource HoverIcon
        {
            get { return (ImageSource)GetValue(HoverIconProperty); }
            set { SetValue(HoverIconProperty, value); }
        }

        /// <summary>
        /// Push 状态图标
        /// </summary>
        public static readonly DependencyProperty PushIconProperty =
           DependencyProperty.Register("PushIcon", typeof(ImageSource), typeof(FXIconTextButtonBase));
        public ImageSource PushIcon
        {
            get { return (ImageSource)GetValue(PushIconProperty); }
            set { SetValue(PushIconProperty, value); }
        }

        /// <summary>
        /// Disable 状态图标
        /// </summary>
        public static readonly DependencyProperty DisableIconProperty =
            DependencyProperty.Register("DisableIcon", typeof(ImageSource), typeof(FXIconTextButtonBase));
        public ImageSource DisableIcon
        {
            get { return (ImageSource)GetValue(DisableIconProperty); }
            set { SetValue(DisableIconProperty, value); }
        }

        /// <summary>
        /// 图标间距
        /// </summary>
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(FXIconTextButtonBase));
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        /// <summary>
        /// 图标宽度
        /// </summary>
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(FXIconTextButtonBase));
        public double IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        /// <summary>
        /// 图标高度
        /// </summary>
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(FXIconTextButtonBase));
        public double IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        #endregion

        #region Text

        /// <summary>
        /// Hover 文字颜色
        /// </summary>
        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register("HoverForeground", typeof(Brush), typeof(FXIconTextButtonBase));
        public Brush HoverForeground
        {
            get { return (Brush)GetValue(HoverForegroundProperty); }
            set { SetValue(HoverForegroundProperty, value); }
        }

        /// <summary>
        /// Push 文字颜色
        /// </summary>
        public static readonly DependencyProperty PushForegroundProperty =
            DependencyProperty.Register("PushForeground", typeof(Brush), typeof(FXIconTextButtonBase));
        public Brush PushForeground
        {
            get { return (Brush)GetValue(PushForegroundProperty); }
            set { SetValue(PushForegroundProperty, value); }
        }

        /// <summary>
        /// Disable 文字颜色
        /// </summary>
        public static readonly DependencyProperty DisableForegroundProperty =
            DependencyProperty.Register("DisableForeground", typeof(Brush), typeof(FXIconTextButtonBase));
        public Brush DisableForeground
        {
            get { return (Brush)GetValue(DisableForegroundProperty); }
            set { SetValue(DisableForegroundProperty, value); }
        }

        /// <summary>
        /// 文字对齐方式
        /// </summary>
        public static readonly DependencyProperty horAlignmentProperty =
            DependencyProperty.Register("horAlignment", typeof(HorizontalAlignment), typeof(FXIconTextButtonBase));
        public HorizontalAlignment horAlignment
        {
            get { return (HorizontalAlignment)GetValue(horAlignmentProperty); }
            set { SetValue(horAlignmentProperty, value); }
        }

        /// <summary>
        /// 文字间距
        /// </summary>
        public static readonly DependencyProperty TextMarginProperty =
            DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(FXIconTextButtonBase));
        public Thickness TextMargin
        {
            get { return (Thickness)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }

        #endregion

        #region Background

        /// <summary>
        /// Hover 背景
        /// </summary>
        public static readonly DependencyProperty HoverBackgroundProperty =
           DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(FXIconTextButtonBase));
        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        /// <summary>
        /// Push 背景
        /// </summary>
        public static readonly DependencyProperty PushBackgroundProperty =
           DependencyProperty.Register("PushBackground", typeof(Brush), typeof(FXIconTextButtonBase));
        public Brush PushBackground
        {
            get { return (Brush)GetValue(PushBackgroundProperty); }
            set { SetValue(PushBackgroundProperty, value); }
        }

        /// <summary>
        /// Disable 背景
        /// </summary>
        public static readonly DependencyProperty DisableBackgroundProperty =
           DependencyProperty.Register("DisableBackground", typeof(Brush), typeof(FXIconTextButtonBase));
        public Brush DisableBackground
        {
            get { return (Brush)GetValue(DisableBackgroundProperty); }
            set { SetValue(DisableBackgroundProperty, value); }
        }

        #endregion

    }
}
