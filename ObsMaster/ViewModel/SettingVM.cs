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
		public int iOldVolume { get; set; } = 50;
		//麦克风音量
		private int _iMicVolume = 50;
		public int iMicVolume
		{
			get { return _iMicVolume; }
			set { Set("iMicVolume", ref _iMicVolume, value); }
		}

		//麦克风是否被禁用状态
		private bool _bMicUnable = false;
		public bool bMicUnable
		{
			get { return _bMicUnable; }
			set { Set("bMicUnable", ref _bMicUnable, value); }
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
		private bool _bVolumeUnable = false;
		public bool bVolumeUnable
		{
			get { return _bVolumeUnable; }
			set { Set("bVolumeUnable", ref _bVolumeUnable, value); }
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
