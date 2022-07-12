using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class LobbyPlayerModel : ModelBase
    {
        private Guid _userId;

        public Guid UserId
        {
            get { return _userId; }
            set
            {
                ValidateProperty(ref _userId, value);
                NotifyPropertyChanged();
            }
        }


        private string _playerName;

        [Required(AllowEmptyStrings = false)]
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                ValidateProperty(ref _playerName, value);
                NotifyPropertyChanged();
            }
        }

        private int? _characterId;

        [IntegerValidator(MinValue = 1, MaxValue = int.MaxValue, ExcludeRange = false)]
        public int? CharacterId
        {
            get { return _characterId; }
            set
            {
                ValidateProperty(ref _characterId, value);
                NotifyPropertyChanged();
            }
        }

        private string _characterName;

        [Required(AllowEmptyStrings = false)]
        public string CharacterName
        {
            get { return _characterName; }
            set
            {
                ValidateProperty(ref _characterName, value);
                NotifyPropertyChanged();
            }
        }

        private string _status;

        [Required(AllowEmptyStrings = false)]
        public string Status
        {
            get { return _status; }
            set
            {
                ValidateProperty(ref _status, value);
                NotifyPropertyChanged();
            }
        }

        private string _playerType;

        [Required(AllowEmptyStrings = false)]
        public string PlayerType
        {
            get { return _playerType; }
            set
            {
                ValidateProperty(ref _playerType, value);
                NotifyPropertyChanged();
            }
        }

        private LobbyCharacterModel _character;

        public LobbyCharacterModel Character
        {
            get { return _character; }
            set
            {
                ValidateProperty(ref _character, value);
                NotifyPropertyChanged();
            }
        }

    }
}
