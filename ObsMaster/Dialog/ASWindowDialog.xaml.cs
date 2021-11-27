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
    /// WinCaptureDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ASWindowDialog 
    {
        private ViewModel.MainVM vm;

        private List<CLI_WindowItem> m_vWindowItems = new List<CLI_WindowItem>();

        public ASWindowDialog()
        {
            InitializeComponent();
            this.Loaded += WinCaptureDialog_Loaded;
        }

        protected override void OnCloseButtonClicked()
        {
            Close();
        }

        protected override void OnMinimizeButtonClicked()
        {
            WindowState = WindowState.Minimized;
        }

        private void WinCaptureDialog_Loaded(object sender, RoutedEventArgs e)
        {
            vm = ViewModel.MainVM.Instance;

            Grid gr = new Grid();
            gr.Background = new SolidColorBrush(Color.FromRgb(0xe0, 0xe1, 0xea));
            HeaderTitleBar.Children.Add(gr);

            TextBlock tb = new TextBlock();
            tb.Text = "添加窗口源";
            tb.Padding = new Thickness(20, 0, 0, 0);
            tb.FontSize = 16;
            tb.VerticalAlignment = VerticalAlignment.Center;
            gr.Children.Add(tb);

            vm.ObsCore.GetWindows(m_vWindowItems);

            foreach (var v in m_vWindowItems)
            {
                cbWindow.Items.Add(v.Name);
            }
            cbWindow.SelectedIndex = 0;

        }

        private void OnWindowClicked(object sender, RoutedEventArgs e)
        {
            var data = ((Button)sender).Tag as Model.MWindowItem;
            if(data != null)
            {
                vm.ObsCore.AddCaptureWindow(data.Name, data.WinName); 
            }
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            var SelectedIndex = cbWindow.SelectedIndex;

            if (SelectedIndex != -1 && SelectedIndex < m_vWindowItems.Count) 
            {
                var it = m_vWindowItems[SelectedIndex];                
                vm.ObsCore.AddCaptureWindow(it.Name, it.WinName);
            }

            Close();
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
