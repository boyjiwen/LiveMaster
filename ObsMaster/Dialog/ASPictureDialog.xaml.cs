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
    public partial class ASPictureDialog 
    {
        private ViewModel.MainVM vm;

        public ASPictureDialog()
        {
            InitializeComponent();
            vm = ViewModel.MainVM.Instance;
            this.Loaded += ASPictureDialog_Loaded;
        }

        private void ASPictureDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Grid gr = new Grid();
            gr.Background = new SolidColorBrush(Color.FromRgb(0xe0, 0xe1, 0xea));
            HeaderTitleBar.Children.Add(gr);

            TextBlock tb = new TextBlock();
            tb.Text = "添加图片源";
            tb.Padding = new Thickness(20, 0, 0, 0);
            tb.FontSize = 16;
            tb.VerticalAlignment = VerticalAlignment.Center;
            gr.Children.Add(tb);
        }

        protected override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();
            Close();
        }

        protected override void OnMinimizeButtonClicked()
        {
            WindowState = WindowState.Minimized;
        }

        private void OnBrowseClick(object sender, RoutedEventArgs e)
        {
            var file = new System.Windows.Forms.OpenFileDialog();
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string v = file.FileName;
                tbFilePath.Text = v;
                imgFile.Source = new BitmapImage(new Uri(v, UriKind.Absolute));
            }
        }


        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
