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
    public partial class SceneRenameDialog
	{
        private ViewModel.MainVM vm;

		public int iResult { get; set; } = -1;

		public SceneRenameDialog()
        {
            InitializeComponent();
            vm = ViewModel.MainVM.Instance;
            this.Loaded += SceneRenameDialog_Loaded;
        }

		private void SceneRenameDialog_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				MaximizeButton.Visibility = Visibility.Collapsed;
				MinimizeButton.Visibility = Visibility.Collapsed;

				Grid gr = new Grid();
				gr.Background = new SolidColorBrush(Color.FromRgb(0xe0, 0xe1, 0xea));
				HeaderTitleBar.Children.Add(gr);

				TextBlock tb = new TextBlock();
				tb.Text = "重命名";
				tb.Padding = new Thickness(20, 0, 0, 0);
				tb.FontSize = 16;
				tb.VerticalAlignment = VerticalAlignment.Center;
				gr.Children.Add(tb);
			}
			catch { }
		}

		public void setSceneName(string name)
		{
			try
			{
				this.SceneNameTextBox.Text = name;
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

		private void BtnCancel(object sender, RoutedEventArgs e)
		{
			try
			{
				OnCloseButtonClicked();
			}
			catch { }
		}

		private void BtnOk(object sender, RoutedEventArgs e)
		{
			try
			{
				iResult = 0;
				OnCloseButtonClicked();
			}
			catch { }	
		}
	}
}
