using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HuyaFX.UI.Controls.Buttons
{
    public class FXImageRadio : RadioButton
    {
        static FXImageRadio()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXImageRadio), new FrameworkPropertyMetadata(typeof(FXImageRadio)));
        }

        //  未选中
        public static readonly DependencyProperty UnSelectedIconProperty =
            DependencyProperty.Register("UnSelectedIcon", typeof(ImageSource), typeof(FXImageRadio));

        public ImageSource UnSelectedIcon
        {
            get { return (ImageSource)GetValue(UnSelectedIconProperty); }
            set { SetValue(UnSelectedIconProperty, value); }
        }

        //  未选中hover
        public static readonly DependencyProperty UnSelectedIconHoverProperty =
            DependencyProperty.Register("UnSelectedIconHover", typeof(ImageSource), typeof(FXImageRadio));

        public ImageSource UnSelectedIconHover
        {
            get { return (ImageSource)GetValue(UnSelectedIconHoverProperty); }
            set { SetValue(UnSelectedIconHoverProperty, value); }
        }

        //  选中
        public static readonly DependencyProperty SelectedIconProperty =
            DependencyProperty.Register("SelectedIcon", typeof(ImageSource), typeof(FXImageRadio));

        public ImageSource SelectedIcon
        {
            get { return (ImageSource)GetValue(SelectedIconProperty); }
            set { SetValue(SelectedIconProperty, value); }
        }

        //  选中hover
        public static readonly DependencyProperty SelectedIconHoverProperty =
            DependencyProperty.Register("SelectedIconHover", typeof(ImageSource), typeof(FXImageRadio));

        public ImageSource SelectedIconHover
        {
            get { return (ImageSource)GetValue(SelectedIconHoverProperty); }
            set { SetValue(SelectedIconHoverProperty, value); }
        }

        //  图片宽度
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(int), typeof(FXImageRadio));

        public int IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        //图片高度
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(int), typeof(FXImageRadio));
        public int IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
    }
}
