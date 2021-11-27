using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsMaster.ViewModel
{
    public partial class MainVM : UICore.ViewModelBase
    {
        private static MainVM _instance;
        public static MainVM Instance

        {
            get { return _instance ?? (_instance = new MainVM()); }
        }

        public ObsWrapper ObsCore { get; set; } = new ObsWrapper();


        public void InitEvent()
        {
            ObsCore.OnEventReloadSceneItemList += ObsCore_OnEventReloadSceneItemList;
            ObsCore.OnEventCurrentScene += ObsCore_OnEventCurrentScene;


        }

        private string _sceneName;
        public string SceneName
        {
            get { return _sceneName; }
            set { Set("SceneName", ref _sceneName, value);  }
        }

        public ObservableCollection<Model.MSceneData> VSceneMenuItems { get; set; } = new ObservableCollection<Model.MSceneData>();


        private void ObsCore_OnEventCurrentScene(string name)
        {
            SceneName = name;
        }

        public ObservableCollection<Model.MSceneItem> VSceneItems { get; set; } = new ObservableCollection<Model.MSceneItem>();

        private void ObsCore_OnEventReloadSceneItemList(List<SceneItem> li)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                VSceneItems.Clear();
                foreach (var item in li)
                {
                    VSceneItems.Add(new Model.MSceneItem() { Name = item.Name, Selected = item.bSelected, Visible = item.bVisible, Id = item.Id });
                }
            }));


        }
    }
}
