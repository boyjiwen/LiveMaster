using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsMaster.Model
{
    public class MSceneData: UICore.ViewModelBase
    {
        public int Index { get; set; }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { Set("Name", ref _name, value); }
        }
    }

    public class MSceneItem : UICore.ViewModelBase
    {
        private string _name = "";
        public string Name
		{
			get { return _name; }
            set { Set("Name", ref _name, value); }
        }

		public string Id { get; set; }

        private bool _visible = true;
        public bool Visible
        {
            get { return _visible; }
            set { Set("Visible", ref _visible, value); }
        }

        //public bool Selected { get; set; }
        private bool _selected = false;
        public bool Selected
        {
            get { return _selected; }
            set { Set("Selected", ref _selected, value); }
        }

    }

    public class MScreenItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MWindowItem
    {       
        public string Name { get; set; }
        public string WinName { get; set; }
    }

}
