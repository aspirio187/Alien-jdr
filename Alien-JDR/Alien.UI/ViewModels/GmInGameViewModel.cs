using Alien.Socket.Models;
using Alien.UI.Helpers;
using Alien.UI.States;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class GmInGameViewModel : ViewModelBase
    {
        public SocketRouter SocketRouteur { get; set; }

        public GmInGameViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {
            SocketRouteur = new SocketRouter().Start();

            SocketRouteur.SendOn(Global.CHANEL_PING, "Mon message");

        }

        public void InitialiseChanels()
        {
            SocketRouteur.On(Global.CHANEL_PING, ChanelPing);
        }

        public bool ChanelPing(dynamic cli, Message args)
        {
            return true;
        }
    }
}
