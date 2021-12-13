using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class LobbyModel : ModelBase
    {
        public int Id { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                ValidateProperty(ref _name, value);
                NotifyPropertyChanged();
            }
        }

        private int _maximumPlayers;

        public int MaximumPlayers
        {
            get { return _maximumPlayers; }
            set
            {
                ValidateProperty(ref _maximumPlayers, value);
                NotifyPropertyChanged();
            }
        }

        private string _mode;

        public string Mode
        {
            get { return _mode; }
            set
            {
                ValidateProperty(ref _mode, value);
                NotifyPropertyChanged();
            }
        }


        private string _hostIp;

        public string HostIp
        {
            get { return _hostIp; }
            set
            {
                ValidateProperty(ref _hostIp, value);
                NotifyPropertyChanged();
            }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set
            {
                ValidateProperty(ref _status, value);
                NotifyPropertyChanged();
            }
        }

    }
}
