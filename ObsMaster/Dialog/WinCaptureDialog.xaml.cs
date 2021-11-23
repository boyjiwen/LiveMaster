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
    public partial class WinCaptureDialog : Window
    {
        private ViewModel.MainVM vm;

        public WinCaptureDialog()
        {
            InitializeComponent();
            this.Loaded += WinCaptureDialog_Loaded;
        }

        private void WinCaptureDialog_Loaded(object sender, RoutedEventArgs e)
        {
            vm = (ViewModel.MainVM)DataContext;
        }

        private void OnWindowClicked(object sender, RoutedEventArgs e)
        {
            var data = ((Button)sender).Tag as Model.MWindowItem;
            if(data != null)
            {
                vm.ObsCore.AddCaptureWindow(data.Name, data.WinName); 
            }
        }
    }
}
