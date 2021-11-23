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
    /// CreateSceneDialog.xaml 的交互逻辑
    /// </summary>
    public partial class CreateSceneDialog : Window
    {
        public int iResult { get; set; } = -1;
        public string sSceneName { get; set; } = "";

        public CreateSceneDialog()
        {
            InitializeComponent();
            OkButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            iResult = 0;
            sSceneName = TbScene.Text;

            Close();
        }
    }
}
