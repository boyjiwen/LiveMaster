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
        private Action ActSaveConfig;

        public SettingDialog()
        {
            InitializeComponent();
            vm = ViewModel.MainVM.Instance;
            this.Loaded += SettingDialog_Loaded;

        }

        protected override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();

            //  SaveStreamConfig();

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


            pushRadio.IsChecked = true;
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

        private void GetResolution(string ss, out ulong x, out ulong y)
        {
            int i1 = ss.IndexOf('x');
            int i2 = ss.IndexOf(' ');

            string s1 = ss.Substring(0, i1);
            string s2 = ss.Substring(i1 + 1, i2 - i1);

            x = ulong.Parse(s1);
            y = ulong.Parse(s2);
        }

        private void InitVideoConfig() 
        {
            //  base    
            var cx = vm.ObsCore.GetConfigUInt("Video", "BaseCX");
            var cy = vm.ObsCore.GetConfigUInt("Video", "BaseCY");
            var ocx = vm.ObsCore.GetConfigUInt("Video", "OutputCX");
            var ocy = vm.ObsCore.GetConfigUInt("Video", "OutputCY");
            var fps = vm.ObsCore.GetConfigUInt("Video", "FPSInt");
            var rate = vm.ObsCore.GetConfigUInt("SimpleOutput", "VBitrate");
            var encoder = vm.ObsCore.GetConfigString("SimpleOutput", "StreamEncoder");

            tbFPS.Text = fps > 10 ? fps.ToString() : "30";

            string scxy = string.Format($"{cx}x{cy}  {cy}P");
            string soxy = string.Format($"{ocx}x{ocy}  {ocy}P");
            cbBaseResolution.Text = scxy;
            cbOutResolution.Text = soxy;
            tbRate.Text = rate.ToString();
            cbEncoder.Text = encoder == "x264" ? "软件编码" : "硬件编码";
        }

        private void SaveVideoConfig()
        {
            GetResolution(cbBaseResolution.Text, out var cx, out var cy);
            vm.ObsCore.SetConfigUInt("Video", "BaseCX", cx);
            vm.ObsCore.SetConfigUInt("Video", "BaseCY", cy);

            GetResolution(cbOutResolution.Text, out var ox, out var oy);
            vm.ObsCore.SetConfigUInt("Video", "OutputCX", ox);
            vm.ObsCore.SetConfigUInt("Video", "OutputCY", oy);

            ulong.TryParse(tbFPS.Text, out var fps);
            vm.ObsCore.SetConfigUInt("Video", "FPSInt", fps);

            ulong.TryParse(tbRate.Text, out var rate);
            vm.ObsCore.SetConfigUInt("SimpleOutput", "VBitrate", rate);

            var sEncoder = cbEncoder.Text == "软件编码" ? "x264" : "qsv";
            vm.ObsCore.SetConfigString("SimpleOutput", "StreamEncoder", sEncoder);
        }

        private void InitAudioConfig()
        {


        }

        private void SaveAudioConfig()
        {

        }

        private void InitRecordConfig()
        {
            var path = vm.ObsCore.GetConfigString("AdvOut", "RecFilePath");
            var formet = vm.ObsCore.GetConfigString("AdvOut", "RecFormat");

            tbRecordPath.Text = path;
            cbRecordFormat.Text = formet;
        }

        private void SaveRecordConfig()
        {
            vm.ObsCore.SetConfigString("AdvOut", "RecFilePath", tbRecordPath.Text);
            vm.ObsCore.SetConfigString("AdvOut", "RecFormat", cbRecordFormat.Text);
        }


        private void OnConfirmClick(object sender, RoutedEventArgs e)
        {
            ActSaveConfig?.Invoke();
        }

        private void pushRadio_Checked(object sender, RoutedEventArgs e)
        {
            InitStreamConfig();
            ActSaveConfig = SaveStreamConfig;
        }        

        private void videoRadio_Checked(object sender, RoutedEventArgs e)
        {
            InitVideoConfig();
            ActSaveConfig = SaveVideoConfig;
        }

        private void audioRadio_Checked(object sender, RoutedEventArgs e)
        {
            InitAudioConfig();
            ActSaveConfig = SaveAudioConfig;
        }

        private void recordRadio_Checked(object sender, RoutedEventArgs e)
        {
            InitRecordConfig();
            ActSaveConfig = SaveRecordConfig;
        }
    }
}
