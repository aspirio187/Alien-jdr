using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class LobbyUserModel : ModelBase
    {
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set
            {
                ValidateProperty(ref _id, value);
                NotifyPropertyChanged();
            }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                ValidateProperty(ref _username, value);
                NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"{Username}";
        }
    }
}
