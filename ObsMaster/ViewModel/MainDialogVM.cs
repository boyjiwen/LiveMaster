using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UICore;

namespace ObsMaster.ViewModel
{
    public partial class MainVM
    {
        public MainWindow MainWnd { get; set; } 

        //  private Dialog.MonitorDialog m_monitorWnd;


        //  创建场景
        public void ShowCreateScene()
        {
            var sceneWnd = new Dialog.CreateSceneDialog();
            sceneWnd.Owner = MainWnd;
            sceneWnd.ShowDialog();

            if (sceneWnd.iResult == 0)
            {   
                ObsCore.CreateScene(sceneWnd.sSceneName);
            }
        }

        //  设置对话框
        private DelegateCommand _settingDialogCmd;
        public DelegateCommand SettingDialogCmd { 
            get {
                return _settingDialogCmd ?? (_settingDialogCmd = new DelegateCommand((obj) =>
                {
                    var dlg = new Dialog.SettingDialog();
                    dlg.DataContext = this;
                    dlg.Owner = MainWnd;   
                    
                    dlg.ShowDialog();
                }));
            } 
        }     
        

        //  屏幕捕捉
        public ObservableCollection<Model.MMonitorItem> VMonitorItems { get; set; } = new ObservableCollection<Model.MMonitorItem>();
        public void ShowMonitor()
        {
            var monitorWnd = new Dialog.MonitorDialog();
            monitorWnd.DataContext = this;
            monitorWnd.Owner = MainWnd;

            List<CLI_MinitorItem> ls = new List<CLI_MinitorItem>();
            ObsCore.GetMonitors(ls);

            VMonitorItems.Clear();
            foreach (var v in ls)
            {
                VMonitorItems.Add(new Model.MMonitorItem() { Id = v.Id, Name = v.Name, });
            }

            monitorWnd.ShowDialog();
        }

        private DelegateCommand _monitorCaptureCmd; 
        public DelegateCommand MonitorCaptureCmd
        {
            get {
                return _monitorCaptureCmd ?? (_monitorCaptureCmd = new DelegateCommand((obj) =>
                {
                    var monitorWnd = new Dialog.MonitorDialog();
                    monitorWnd.DataContext = this;
                    monitorWnd.Owner = MainWnd;

                    List<CLI_MinitorItem> ls = new List<CLI_MinitorItem>();
                    ObsCore.GetMonitors(ls);

                    VMonitorItems.Clear();
                    foreach (var v in ls)
                    {
                        VMonitorItems.Add(new Model.MMonitorItem() { Id = v.Id, Name = v.Name, });
                    }

                    monitorWnd.ShowDialog();
                }));
            }
            
        }

        //  窗口捕捉
        public ObservableCollection<Model.MWindowItem> VCaptureWindowItems { get; set; } = new ObservableCollection<Model.MWindowItem>();
        public void ShowCaptureWindow()
        {
            var captureWnd = new Dialog.WinCaptureDialog();
            captureWnd.DataContext = this;
            captureWnd.Owner = MainWnd;

            List<CLI_WindowItem> ls = new List<CLI_WindowItem>();
            ObsCore.GetWindows(ls);

            VCaptureWindowItems.Clear();
            foreach(var v in ls)
            {
                VCaptureWindowItems.Add(new Model.MWindowItem() { Name = v.Name, WinName = v.WinName, });
            }

            captureWnd.ShowDialog();
        }

        private DelegateCommand _windowCaptureCmd;
        public DelegateCommand WindowCaptureCmd
        {
            get
            {
                return _windowCaptureCmd ?? (_windowCaptureCmd = new DelegateCommand((obj) =>
                {
                    var captureWnd = new Dialog.WinCaptureDialog();
                    captureWnd.DataContext = this;
                    captureWnd.Owner = MainWnd;

                    List<CLI_WindowItem> ls = new List<CLI_WindowItem>();
                    ObsCore.GetWindows(ls);

                    VCaptureWindowItems.Clear();
                    foreach (var v in ls)
                    {
                        VCaptureWindowItems.Add(new Model.MWindowItem() { Name = v.Name, WinName = v.WinName, });
                    }

                    captureWnd.ShowDialog();
                }));
            }
        }


        //  图片捕捉
        private DelegateCommand _pictureCaptureCmd;
        public DelegateCommand PictureCaptureCmd
        {
            get
            {
                return _pictureCaptureCmd ?? (_pictureCaptureCmd = new DelegateCommand((obj) =>
                {
                    var wnd = new Dialog.ASPictureDialog();
                    wnd.DataContext = this; 
                    wnd.Owner = MainWnd;    
                    wnd.Show();
                }));
            }
        }



        //  开始开播
        public void StartStream()
        {
            ObsCore.StartStream();
        }

        //  停止开播
        public void StopStream()
        {
            //  ObsCore.
        }

    }
}
