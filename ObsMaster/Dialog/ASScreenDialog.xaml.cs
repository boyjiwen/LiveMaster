using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// MonitorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ASScreenDialog 
    {
        private ViewModel.MainVM vm;
        public int iResult { get; set; } = -1;
        public int SelectedIndex { get; set; }
        public bool IsCaptureMouse { get; set; }


        private List<CLI_MinitorItem> m_vScreenItems = new List<CLI_MinitorItem>();


        public ASScreenDialog()
        {
            InitializeComponent();
            vm = ViewModel.MainVM.Instance;
            this.Loaded += ScreenDialog_Loaded;
        }

        private void ScreenDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Grid gr = new Grid();
            gr.Background = new SolidColorBrush(Color.FromRgb(0xe0, 0xe1, 0xea));
            HeaderTitleBar.Children.Add(gr);

            TextBlock tb = new TextBlock();
            tb.Text = "添加显示器源";
            tb.Padding = new Thickness(20, 0, 0, 0);
            tb.FontSize = 16;
            tb.VerticalAlignment = VerticalAlignment.Center;
            gr.Children.Add(tb);
                        
            vm.ObsCore.GetMonitors(m_vScreenItems);
            foreach (var v in m_vScreenItems)
            {
                cbScreen.Items.Add(v.Name);
            }
            cbScreen.SelectedIndex = 0;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        protected override void OnCloseButtonClicked()
        {
            Close();
        }

        protected override void OnMinimizeButtonClicked()
        {
            WindowState = WindowState.Minimized;
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            iResult = 0;
            SelectedIndex = cbScreen.SelectedIndex;
            IsCaptureMouse = (bool)checkMouse.IsChecked;

            if (SelectedIndex != -1 && SelectedIndex < m_vScreenItems.Count)
            {
                var it = m_vScreenItems[SelectedIndex];
                vm.ObsCore.AddCaptureScreen(it.Name, it.Id, IsCaptureMouse);
            }
            Close();
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        
    }
}
