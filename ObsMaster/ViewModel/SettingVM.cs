using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UICore;


namespace ObsMaster.ViewModel
{
	public partial class MainVM : ViewModelBase
	{
		public enum MicVolume
		{
			OUTPUT_AUDIO_CHANNEL1 = 1,
			OUTPUT_AUDIO_CHANNEL2 = 2,

			INPUT_AUDIO_CHANNEL1 = 3,
			INPUT_AUDIO_CHANNEL2 = 4,
			INPUT_AUDIO_CHANNEL3 = 5,
		}
		public int iOldVolume { get; set; } = 50;
		//麦克风音量
		private int _iMicVolume = 50;
		public int iMicVolume
		{
			get { return _iMicVolume; }
			set { Set("iMicVolume", ref _iMicVolume, value); }
		}

		//麦克风是否被禁用状态
		private bool _bMicState = false;
		public bool bMicState
		{
			get { return _bMicState; }
			set { Set("bMicState", ref _bMicState, value); }
		}

		//音量
		public int iOldMicVolume { get; set; } = 50;
		private int _iVolume = 50;
		public int iVolume
		{
			get { return _iVolume; }
			set { Set("iVolume", ref _iVolume, value); }
		}

		//音量是否被禁用状态
		private bool _bSoundState = false;
		public bool bSoundState
		{
			get { return _bSoundState; }
			set { Set("bSoundState", ref _bSoundState, value); }
		}

		public void InitMicVolume()
		{
			var volume = ObsCore.GetVolume((int)MicVolume.OUTPUT_AUDIO_CHANNEL1);
			iVolume = volume;
		}

		public void UpdateAudioState()
		{
			try
			{
				var _BSoundState = ObsCore.GetMuted((int)MicVolume.OUTPUT_AUDIO_CHANNEL1);
				bSoundState = _BSoundState;

				var _BMicState = ObsCore.GetMuted((int)MicVolume.INPUT_AUDIO_CHANNEL1);
				bMicState = _BMicState;
			}
			catch { }
		}

		//房间号
		private string _sRoomId = "";
		public string sRoomId
		{
			get { return _sRoomId; }
			set { Set("sRoomId", ref _sRoomId, value); }
		}

		//平台名
		private string _sPlatformName = "";
		public string sPlatformName
		{
			get { return _sPlatformName; }
			set { Set("sPlatformName", ref _sPlatformName, value); }
		}
	}
}
