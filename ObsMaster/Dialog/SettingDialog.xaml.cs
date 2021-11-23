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
using System.Windows.Shapes;

namespace ObsMaster.Dialog
{
    /// <summary>
    /// SettingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SettingDialog 
    {
        public SettingDialog()
        {
            InitializeComponent();
            this.Loaded += SettingDialog_Loaded;
        }

        protected override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();
            Close(); 
        }

        private void SettingDialog_Loaded(object sender, RoutedEventArgs e)
        {
            MaximizeButton.Visibility = Visibility.Collapsed;
            MinimizeButton.Visibility = Visibility.Collapsed;   

            Grid gr = new Grid();
            //gr.HorizontalAlignment = HorizontalAlignment.Left;
            gr.Background = new SolidColorBrush(Color.FromRgb(0xe0, 0xe1, 0xea));
            HeaderTitleBar.Children.Add(gr);

            TextBlock tb = new TextBlock();
            tb.Text = "设置中心";
            tb.Padding = new Thickness(20, 0, 0, 0);  
            tb.FontSize = 16;
            tb.VerticalAlignment = VerticalAlignment.Center;
            gr.Children.Add(tb);
            
        }
    }
}
