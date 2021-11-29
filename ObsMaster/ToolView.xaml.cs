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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObsMaster
{
    /// <summary>
    /// ToolView.xaml 的交互逻辑
    /// </summary>
    public partial class ToolView : UserControl
    {
		private ViewModel.MainVM vm;
		public ToolView()
        {
            InitializeComponent();
			vm = ViewModel.MainVM.Instance;
			vm.UpdateAudioState();
		}

		private async void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			try
			{
				await NotifyVolumeChanged();
			}
			catch { }
		}

		private bool bVolumeRun = false;
		private async Task NotifyVolumeChanged()
		{
			try
			{
				if (bVolumeRun)
					return;
				bVolumeRun = true;

				await Task.Delay(200);
				//vm.NoticeVolume(vm.iVolume);
				if (vm.iVolume > 0)
					vm.iOldVolume = vm.iVolume;

				bVolumeRun = false;
			}
			catch { }
		}

		private async void micSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			try
			{
				await NotifyMicVolumeChanged();
			}
			catch { }
		}
		private bool bMicVolumeRun = false;
		private async Task NotifyMicVolumeChanged()
		{
			try
			{
				if (bMicVolumeRun)
					return;
				bMicVolumeRun = true;

				await Task.Delay(200);
				//vm.NoticeVolume(vm.iVolume);
				if (vm.iMicVolume > 0)
					vm.iOldMicVolume = vm.iMicVolume;

				bMicVolumeRun = false;
			}
			catch { }
		}

		private void MicMuteClicked(object sender, RoutedEventArgs e)
		{
			try
			{
				vm.bMicState = false;
				vm.ObsCore.SetMuted(3, false);
			}
			catch { }
		}

		private void MicClicked(object sender, RoutedEventArgs e)
		{
			try
			{
				vm.bMicState = true;
				vm.ObsCore.SetMuted(3, true);
			}
			catch { }
		}

		private void VolumeMuteClicked(object sender, RoutedEventArgs e)
		{
			try
			{
				vm.bSoundState = false;
				vm.ObsCore.SetMuted(1, false);
			}
			catch { }
		}

		private void VolumeClicked(object sender, RoutedEventArgs e)
		{
			try
			{
				vm.bSoundState = true;
				vm.ObsCore.SetMuted(1, true);
			}
			catch { }
		}
	}
}
