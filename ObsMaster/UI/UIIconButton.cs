using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObsMaster.UI
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ObsMaster.UI"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ObsMaster.UI;assembly=ObsMaster.UI"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:UIIconButton/>
    ///
    /// </summary>
    public class UIIconButton : Button
    {
        static UIIconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UIIconButton), new FrameworkPropertyMetadata(typeof(UIIconButton)));
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(UIIconButton));
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty HoverIconProperty =
            DependencyProperty.Register("HoverIcon", typeof(ImageSource), typeof(UIIconButton));
        public ImageSource HoverIcon
        {
            get { return (ImageSource)GetValue(HoverIconProperty); }
            set { SetValue(HoverIconProperty, value); }
        }

        public static readonly DependencyProperty PushIconProperty =
            DependencyProperty.Register("PushIcon", typeof(ImageSource), typeof(UIIconButton));
        public ImageSource PushIcon
        {
            get { return (ImageSource)GetValue(PushIconProperty); }
            set { SetValue(PushIconProperty, value); }
        }


        public string sIconPath
        {
            get { return (string)GetValue(sIconPathProperty); }
            set { SetValue(sIconPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for sIconPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty sIconPathProperty =
            DependencyProperty.Register("sIconPath", typeof(string), typeof(UIIconButton), new PropertyMetadata(OnIconPathInit));

        private static void OnIconPathInit(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {


            
        }
    }
}
