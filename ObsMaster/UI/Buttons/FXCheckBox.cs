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
    public class FXCheckBox : CheckBox
    {
        static FXCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXCheckBox), new FrameworkPropertyMetadata(typeof(FXCheckBox)));
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(FXCheckBox));
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty HoverIconProperty = DependencyProperty.Register("HoverIcon", typeof(ImageSource), typeof(FXCheckBox));
        public ImageSource HoverIcon
        {
            get { return (ImageSource)GetValue(HoverIconProperty); }
            set { SetValue(HoverIconProperty, value); }
        }

        public static readonly DependencyProperty CheckIconProperty = DependencyProperty.Register("CheckIcon", typeof(ImageSource), typeof(FXCheckBox));
        public ImageSource CheckIcon
        {
            get { return (ImageSource)GetValue(CheckIconProperty); }
            set { SetValue(CheckIconProperty, value); }
        }

        public static readonly DependencyProperty CheckHoverIconProperty = DependencyProperty.Register("CheckHoverIcon", typeof(ImageSource), typeof(FXCheckBox));
        public ImageSource CheckHoverIcon
        {
            get { return (ImageSource)GetValue(CheckHoverIconProperty); }
            set { SetValue(CheckHoverIconProperty, value); }
        }
    }
}
