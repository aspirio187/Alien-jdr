using Alien.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class NotificationModel : ModelBase
    {
        public int Id { get; set; }

        private Guid _hostId;

        public Guid HostId
        {
            get { return _hostId; }
            set
            {
                ValidateProperty(ref _hostId, value);
                NotifyPropertyChanged();
            }
        }

        private string _lobbyName;

        public string LobbyName
        {
            get { return _lobbyName; }
            set
            {
                ValidateProperty(ref _lobbyName, value);
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

        private string _lobbyHost;

        public string LobbyHost
        {
            get { return _lobbyHost; }
            set
            {
                ValidateProperty(ref _lobbyHost, value);
                NotifyPropertyChanged();
            }
        }

        private DateTime _sentAt;

        public DateTime SentAt
        {
            get { return _sentAt; }
            set
            {
                ValidateProperty(ref _sentAt, value);
                NotifyPropertyChanged();
                NotifyPropertyChanged();
            }
        }

        private NotificationStatusEnum _notificationStatus;

        public NotificationStatusEnum NotificationStatus
        {
            get { return _notificationStatus; }
            set
            {
                ValidateProperty(ref _notificationStatus, value);
                NotifyPropertyChanged();
            }
        }
    }
}
