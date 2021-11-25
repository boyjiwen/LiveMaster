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
        public ObservableCollection<Model.MScreenItem> VMonitorItems { get; set; } = new ObservableCollection<Model.MScreenItem>();
        public void ShowMonitor()
        {
            var monitorWnd = new Dialog.ASScreenDialog();
            monitorWnd.DataContext = this;
            monitorWnd.Owner = MainWnd;            

            monitorWnd.ShowDialog();
        }

        private DelegateCommand _monitorCaptureCmd; 
        public DelegateCommand MonitorCaptureCmd
        {
            get {
                return _monitorCaptureCmd ?? (_monitorCaptureCmd = new DelegateCommand((obj) =>
                {
                    var monitorWnd = new Dialog.ASScreenDialog();
                    monitorWnd.DataContext = this;
                    monitorWnd.Owner = MainWnd;

                    monitorWnd.ShowDialog();
                }));
            }
            
        }

        //  窗口捕捉
        public ObservableCollection<Model.MWindowItem> VCaptureWindowItems { get; set; } = new ObservableCollection<Model.MWindowItem>();
        public void ShowCaptureWindow()
        {
            var captureWnd = new Dialog.ASWindowDialog();
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
                    var captureWnd = new Dialog.ASWindowDialog();
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
                    wnd.ShowDialog();
                    if (wnd.iResult == 0)
                    {
                        ObsCore.AddImageSource(wnd.FilePath, 100);
                    }
                }));
            }
        }

		//摄像头
		private DelegateCommand _cameraCmd;
		public DelegateCommand CameraCmd
        {
			get
			{
				return _cameraCmd ?? (_cameraCmd = new DelegateCommand((obj) =>
				{
					try
					{
                        var wnd = new Dialog.ASCameraDialog();
						wnd.DataContext = this;
						wnd.Owner = MainWnd;
						wnd.ShowDialog();
					}
                    catch { }
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
