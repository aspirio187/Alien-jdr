using Alien.Socket.Models;
using Alien.UI.Helpers;
using Alien.UI.States;
using AutoMapper;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class GmInGameViewModel : ViewModelBase
    {
        public SocketRouter SocketRouteur { get; set; }

        public GmInGameViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper)
            : base(regionNavigationService, authenticator, mapper)
        {
            SocketRouteur = new SocketRouter().Start();

            SocketRouteur.IsIpOnLine("");

            /*InitialiseChanels();*/

        }

        #region Fonctionalitée MJ
        /// <summary>
        /// Retourne un bool sur la présence des joueurs dans la partie.
        /// </summary>
        /// <returns></returns>
        public bool PlayersIsConnected()
        {
            return (SocketRouteur.GetDisconectedIp().Count > 0 ? true : false);
        }

        /// <summary>
        /// Retourne un tableau des IP de joueurs manquant.
        /// </summary>
        /// <returns></returns>
        public List<System.Net.IPAddress> MissingPlayersIp()
        {
            return SocketRouteur.GetDisconectedIp();
        }

        /// <summary>
        /// Retourne un PingAnalitics des joueurs.
        /// </summary>
        /// <returns></returns>
        public PingAnalitics PlayersPing()
        {
            return SocketRouteur.Ping();
        }

        /// <summary>
        /// MJ demande à un joueur ( IP ) de lancer le dé
        /// </summary>
        /// <returns></returns>
        public void PlayerThrowDice(string IP)
        {
            SocketRouteur.SendToOn(IP, Global.PLAYER_THROW_DICE, "").OnReply(TreatPlayerThrowDice);
        }

        /// <summary>
        /// CallBack traitant le résultat du lancer de dé du joueur
        /// </summary>
        /// <param name="cli"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public void TreatPlayerThrowDice(dynamic cli, Message args)
        {
            // BACK : Appliquer les résultat du lancer

        }
        #endregion

        #region Fonctionalitée Joueur
        /// <summary>
        /// Initialisation des chanel du Joueur
        /// </summary>
        public void InitialiseChanels_Joueur()
        {
            // Chanel qui vas recevoir l'instruction du lancer de dé.
            SocketRouteur.On(Global.PLAYER_THROW_DICE, PLAYER_ThrowDice);
        }

        /// <summary>
        /// CallBack permetant d'effectuer un lancé de dé et de comuniquer le résultat au MJ.
        /// </summary>
        /// <param name="cli"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool PLAYER_ThrowDice(dynamic cli, Message arg) {
            /// BACK : Effectuer le lancement de dé afin de le communiquer au MJ
            return cli.Reply("Résultat du lancement de dé");
        }
        #endregion
    }
}
