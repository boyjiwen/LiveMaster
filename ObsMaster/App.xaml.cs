using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ObsMaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ViewModel.MainVM.Instance.ObsCore.Init();
            ViewModel.MainVM.Instance.ObsCore.InitObs();
            ViewModel.MainVM.Instance.InitEvent();

        }

    }
}
