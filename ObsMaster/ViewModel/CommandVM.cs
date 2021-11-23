using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UICore;


namespace ObsMaster.ViewModel
{
    public partial class MainVM
    {
        private DelegateCommand _changeSceneCmd;
        public DelegateCommand ChangeSceneCmd
        {
            get {
                return _changeSceneCmd ?? (_changeSceneCmd = new DelegateCommand((obj) =>
                {
                    Model.MSceneData data = (Model.MSceneData)obj;
                    if(data != null)
                    {
                        ObsCore.ChangeScene(data.Index);
                    }
                }));
            }            
        }

        //  创建场景
        private DelegateCommand _createSceneCmd;
        public DelegateCommand CreateSceneCmd
        {
            get
            {
                return _createSceneCmd ?? (_createSceneCmd = new DelegateCommand((obj) =>
                {
                    ShowCreateScene();
                }));
            }
        }

        private DelegateCommand _sceneItemVisibleCmd;
        public DelegateCommand SceneItemVisibleCmd
        {
            get
            {
                return _sceneItemVisibleCmd ?? (_sceneItemVisibleCmd = new DelegateCommand((obj) =>
                {
                    Model.MSceneItem item = (Model.MSceneItem)obj;



                }));                
            }
        }




    }
}
