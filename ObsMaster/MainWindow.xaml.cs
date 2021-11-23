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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ObsMaster.ViewModel;

namespace ObsMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
    public partial class MainWindow 
    {
        private MainVM vm = null;
        private TitleView titleView = null;

        public MainWindow()
        {             
            vm = MainVM.Instance;
            InitializeComponent();
            DataContext = vm;
            vm.MainWnd = this;            
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            int w = obsView.HostPanel.Width;
            int h = obsView.HostPanel.Height;

            vm.ObsCore.InitWindow((int)obsView.HostPanel.Handle.ToInt64(), w, h);
            vm.ObsCore.LoadScene();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (titleView == null)
            {
                titleView = new TitleView();
                HeaderTitleBar.Children.Add(titleView); 
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            vm.ObsCore.SaveProject();
            vm.ObsCore.SaveConfig();

            base.OnClosed(e);
        }

        protected override void OnCloseButtonClicked()
        {
            Close();
        }

        protected override void OnMinimizeButtonClicked()
        {
            this.WindowState = WindowState.Minimized;
        }


        //private void FormsHost_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    //int x = HostPanel.Left;
        //    //int y = HostPanel.Top;
        //    //int w = HostPanel.Width;
        //    //int h = HostPanel.Height;

        //    //vm.ObsCore.SetWindowPos(x, y, w, h);
        //    //MainVM.Instance.ObsCore.SetWindowPos(x, y, w, h);
        //}

        private void OnMonitorClicked(object sender, RoutedEventArgs e)
        {
            vm.ShowMonitor();
        }

        private void OnWindowClicked(object sender, RoutedEventArgs e)
        {
            vm.ShowCaptureWindow();
        }

        private void UISceneListboxItem_ShowButtonClicked(object sender, RoutedEventArgs e)
        {
            var data = ((UI.UISceneListboxItem)sender).DataContext as Model.MSceneItem;
            if (data != null)
            {
                int idx = vm.VSceneItems.IndexOf(data);
                data.Visible = false;
                vm.ObsCore.SetSceneItemVisibile(idx, false);
            }
        }
        private void UISceneListboxItem_HideButtonClicked(object sender, RoutedEventArgs e)
        {
            var data = ((UI.UISceneListboxItem)sender).DataContext as Model.MSceneItem;
            if(data != null)
            {
                int idx = vm.VSceneItems.IndexOf(data);
                data.Visible = true;
                vm.ObsCore.SetSceneItemVisibile(idx, true);
            }
        }

        private void UISceneListboxItem_DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            var data = ((UI.UISceneListboxItem)sender).DataContext as Model.MSceneItem;
            if (data != null)
            {
                int idx = vm.VSceneItems.IndexOf(data);
                data.Visible = true;
                vm.ObsCore.RemoveSceneItem(idx); 
            }
        }

        private void UISceneListboxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UISceneListboxItem_Click(object sender, RoutedEventArgs e)
        {
            Model.MSceneItem data = ((UI.UISceneListboxItem) sender).DataContext as Model.MSceneItem;
            if (data != null)
            {
                int idx = vm.VSceneItems.IndexOf(data);
                if (idx != -1)
                    vm.ObsCore.SelectSceneItem(idx);

            }


        }

        private void OnStartLiveClicked(object sender, RoutedEventArgs e)
        {
            vm.StartStream();
        }

        private void OnStopLiveClicked(object sender, RoutedEventArgs e)
        {

        }

        private void AddSceneClicked(object sender, RoutedEventArgs e)
        {
            List<SceneData> sceneDatas = new List<SceneData>();
            vm.ObsCore.GetScenes(sceneDatas);

            vm.VSceneMenuItems.Clear();
            foreach (SceneData v in sceneDatas)
            {
                vm.VSceneMenuItems.Add(new Model.MSceneData() { Index = v.index, Name = v.name });
            }

            ScenePopup.IsOpen = true;
        }

        private void PopupHideClicked(object sender, RoutedEventArgs e)
        {
            ScenePopup.IsOpen = false;
        }
    }
}
