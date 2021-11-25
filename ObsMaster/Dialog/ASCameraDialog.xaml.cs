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
    /// ASPictureDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ASCameraDialog
    {
        private ViewModel.MainVM vm;

        public ASCameraDialog()
        {
            InitializeComponent();
            vm = ViewModel.MainVM.Instance;
            this.Loaded += ASCameraDialog_Loaded;
        }

		private void ASCameraDialog_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				Grid gr = new Grid();
				gr.Background = new SolidColorBrush(Color.FromRgb(0xe0, 0xe1, 0xea));
				HeaderTitleBar.Children.Add(gr);

				TextBlock tb = new TextBlock();
				tb.Text = "摄像头";
				tb.Padding = new Thickness(20, 0, 0, 0);
				tb.FontSize = 16;
				tb.VerticalAlignment = VerticalAlignment.Center;
				gr.Children.Add(tb);
			}
			catch { }
		}

		protected override void OnCloseButtonClicked()
		{
			try
			{
				base.OnCloseButtonClicked();
				Close();
			}
			catch { }
		}

		protected override void OnMinimizeButtonClicked()
		{
			try
			{
				WindowState = WindowState.Minimized;
			}
			catch { }
		}

		private void OnOkClick(object sender, RoutedEventArgs e)
		{
			try
			{

			}
			catch { }
		}

		private void OnCloseClick(object sender, RoutedEventArgs e)
		{
			try
			{

			}
			catch { }
		}
	}
}
