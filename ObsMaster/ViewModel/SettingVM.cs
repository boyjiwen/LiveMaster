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

		//音量
		public int iOldMicVolume { get; set; } = 50;
		private int _iVolume = 50;
		public int iVolume
		{
			get { return _iVolume; }
			set { Set("iVolume", ref _iVolume, value); }
		}

	}
}
