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

namespace ObsMaster
{
    /// <summary>
    /// ObsView.xaml 的交互逻辑
    /// </summary>
    public partial class ObsView : UserControl
    {
        public ObsView()
        {
            InitializeComponent();
        }

        private void FormsHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int x = HostPanel.Left;
            int y = HostPanel.Top;
            int w = HostPanel.Width;
            int h = HostPanel.Height;

            ViewModel.MainVM.Instance.ObsCore.SetWindowPos(x, y, w, h);
        }
    }
}
