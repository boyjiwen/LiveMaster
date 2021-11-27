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
    ///     <MyNamespace:UISceneListboxItem/>
    ///
    /// </summary>
    public class UISceneListboxItem : RadioButton
    {
        private Button _deleteButton;
        private Button _showButton;
        private Button _hideButton;

        public event RoutedEventHandler DeleteButtonClicked;
        public event RoutedEventHandler ShowButtonClicked;
        public event RoutedEventHandler HideButtonClicked;



        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(UISceneListboxItem));



        public ImageSource IdIcon
        {
            get { return (ImageSource)GetValue(IdIconProperty); }
            set { SetValue(IdIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IdIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdIconProperty =
            DependencyProperty.Register("IdIcon", typeof(ImageSource), typeof(UISceneListboxItem));



        static UISceneListboxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UISceneListboxItem), new FrameworkPropertyMetadata(typeof(UISceneListboxItem)));
        }

        public override void OnApplyTemplate()
        {
            _showButton = GetTemplateChild("btnShow") as Button;
            _deleteButton = GetTemplateChild("btnDelete") as Button;
            _hideButton = GetTemplateChild("btnHide") as Button;

            _showButton.Click += OnShowButtonClick;
            _deleteButton.Click += OnDeleteButtonClick;
            _hideButton.Click += OnHideButtonClick;

            this.MouseLeftButtonDown += UISceneListboxItem_MouseLeftButtonDown;


            base.OnApplyTemplate();
        }

        private void UISceneListboxItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int d = 0;
        }

        private void OnHideButtonClick(object sender, RoutedEventArgs e)
        {
            HideButtonClicked?.Invoke(this, e); 
        }

        private void OnShowButtonClick(object sender, RoutedEventArgs e)
        {
            ShowButtonClicked?.Invoke(this, e);
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            DeleteButtonClicked?.Invoke(this, e);
        }


    }
}
