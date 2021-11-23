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
        private ViewModel.MainVM vm;

        public SettingDialog()
        {
            InitializeComponent();
            vm = ViewModel.MainVM.Instance;
            this.Loaded += SettingDialog_Loaded;

        }

        protected override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();

            SaveStreamConfig();

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
                                                   
        private void InitStreamConfig()
        {
            tbStreamUrl.Text = vm.ObsCore.GetConfigString("Stream", "Url");
            tbStreamPassword.Text = vm.ObsCore.GetConfigString("Stream", "Name");
        }

        private void SaveStreamConfig()
        {
            string vUrl = tbStreamUrl.Text;
            string vKey = tbStreamPassword.Text;

            vm.ObsCore.SetConfigString("Stream", "Url", vUrl);
            vm.ObsCore.SetConfigString("Stream", "Name", vKey);
        }


        private void IsStreamViewVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (bool.Parse(e.NewValue.ToString()))
            {
                InitStreamConfig();
            }
        }
    }
}
