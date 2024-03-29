﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HuyaFX.UI.Controls.Buttons
{
    public class FXButtonBase : Button
    {
        #region Icon

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(FXButtonBase));
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty HoverIconProperty =
            DependencyProperty.Register("HoverIcon", typeof(ImageSource), typeof(FXButtonBase));
        public ImageSource HoverIcon
        {
            get { return (ImageSource)GetValue(HoverIconProperty); }
            set { SetValue(HoverIconProperty, value); }
        }

        public static readonly DependencyProperty PushIconProperty =
            DependencyProperty.Register("PushIcon", typeof(ImageSource), typeof(FXButtonBase));
        public ImageSource PushIcon
        {
            get { return (ImageSource)GetValue(PushIconProperty); }
            set { SetValue(PushIconProperty, value); }
        }

        public static readonly DependencyProperty DisableIconProperty =
            DependencyProperty.Register("DisableIcon", typeof(ImageSource), typeof(FXButtonBase));
        public ImageSource DisableIcon
        {
            get { return (ImageSource)GetValue(DisableIconProperty); }
            set { SetValue(DisableIconProperty, value); }
        }

        /// <summary>
        /// 图标间距
        /// </summary>
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(FXButtonBase));
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        /// <summary>
        /// 图标宽度
        /// </summary>
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(FXButtonBase));
        public double IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        /// <summary>
        /// 图标高度
        /// </summary>
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(FXButtonBase));
        public double IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        #endregion

        #region Background

        /// <summary>
        /// Hover 背景
        /// </summary>
        public static readonly DependencyProperty HoverBackgroundProperty =
           DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(FXButtonBase));
        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        /// <summary>
        /// Push 背景
        /// </summary>
        public static readonly DependencyProperty PushBackgroundProperty =
           DependencyProperty.Register("PushBackground", typeof(Brush), typeof(FXButtonBase));
        public Brush PushBackground
        {
            get { return (Brush)GetValue(PushBackgroundProperty); }
            set { SetValue(PushBackgroundProperty, value); }
        }

        /// <summary>
        /// Disable 背景
        /// </summary>
        public static readonly DependencyProperty DisableBackgroundProperty =
           DependencyProperty.Register("DisableBackground", typeof(Brush), typeof(FXButtonBase));
        public Brush DisableBackground
        {
            get { return (Brush)GetValue(DisableBackgroundProperty); }
            set { SetValue(DisableBackgroundProperty, value); }
        }

        #endregion
    }

    public class FXTextButtonBase : FXButtonBase
    {
        #region Text

        /// <summary>
        /// Hover 文字颜色
        /// </summary>
        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register("HoverForeground", typeof(Brush), typeof(FXTextButtonBase));
        public Brush HoverForeground
        {
            get { return (Brush)GetValue(HoverForegroundProperty); }
            set { SetValue(HoverForegroundProperty, value); }
        }

        /// <summary>
        /// Push 文字颜色
        /// </summary>
        public static readonly DependencyProperty PushForegroundProperty =
            DependencyProperty.Register("PushForeground", typeof(Brush), typeof(FXTextButtonBase));
        public Brush PushForeground
        {
            get { return (Brush)GetValue(PushForegroundProperty); }
            set { SetValue(PushForegroundProperty, value); }
        }

        /// <summary>
        /// Disable 文字颜色
        /// </summary>
        public static readonly DependencyProperty DisableForegroundProperty =
            DependencyProperty.Register("DisableForeground", typeof(Brush), typeof(FXTextButtonBase));
        public Brush DisableForeground
        {
            get { return (Brush)GetValue(DisableForegroundProperty); }
            set { SetValue(DisableForegroundProperty, value); }
        }

        /// <summary>
        /// 文字对齐方式
        /// </summary>
        public static readonly DependencyProperty horAlignmentProperty =
            DependencyProperty.Register("horAlignment", typeof(HorizontalAlignment), typeof(FXTextButtonBase));
        public HorizontalAlignment horAlignment
        {
            get { return (HorizontalAlignment)GetValue(horAlignmentProperty); }
            set { SetValue(horAlignmentProperty, value); }
        }

        /// <summary>
        /// 文字边距
        /// </summary>
        public static readonly DependencyProperty TextMarginProperty =
            DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(FXTextButtonBase));
        public Thickness TextMargin
        {
            get { return (Thickness)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }

        /// <summary>
        /// 文字对齐方式
        /// </summary>
        public static readonly DependencyProperty TextHoriAlignmentProperty =
            DependencyProperty.Register("TextHoriAlignment", typeof(HorizontalAlignment), typeof(FXTextButtonBase));
        public HorizontalAlignment TextHoriAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextHoriAlignmentProperty); }
            set { SetValue(TextHoriAlignmentProperty, value); }
        }

        #endregion

        /// <summary>
        /// 圆角
        /// </summary>
        public static readonly DependencyProperty BorderRadiusProperty =
            DependencyProperty.Register("BorderRadius", typeof(CornerRadius), typeof(FXTextButtonBase));
        public CornerRadius BorderRadius
        {
            get { return (CornerRadius)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }
    }

    public class FXCheckBase : FXTextButtonBase
    {
        #region Check

        /// <summary>
        /// 选中状态
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(FXCheckBase));
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        /// <summary>
        /// Check 状态图标
        /// </summary>
        public static readonly DependencyProperty CheckIconProperty =
          DependencyProperty.Register("CheckIcon", typeof(ImageSource), typeof(FXCheckBase));
        public ImageSource CheckIcon
        {
            get { return (ImageSource)GetValue(CheckIconProperty); }
            set { SetValue(CheckIconProperty, value); }
        }

        /// <summary>
        /// Check 文字颜色
        /// </summary>
        public static readonly DependencyProperty CheckForegroundProperty =
            DependencyProperty.Register("CheckForeground", typeof(Brush), typeof(FXCheckBase));
        public Brush CheckForeground
        {
            get { return (Brush)GetValue(CheckForegroundProperty); }
            set { SetValue(CheckForegroundProperty, value); }
        }

        /// <summary>
        /// Check 背景
        /// </summary>
        public static readonly DependencyProperty CheckBackgroundProperty =
           DependencyProperty.Register("CheckBackground", typeof(Brush), typeof(FXCheckBase));
        public Brush CheckBackground
        {
            get { return (Brush)GetValue(CheckBackgroundProperty); }
            set { SetValue(CheckBackgroundProperty, value); }
        }


        #endregion
        
    }


}
