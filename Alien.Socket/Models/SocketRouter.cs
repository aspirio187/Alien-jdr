using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace Alien.Socket.Models
{
    /// <summary>
    /// SocketRouter est une librairie permetant une communication TCP/IP entre deux processus sur le même réseau.
    /// <para>SocketRouter contient 2 serveurs : </para>
    /// <list type="number">
    /// <item>
    /// <description>Serveur Listener.</description>
    /// <para>Son rôle est d'écouter une requête entrante. Il ne peux pas s'adresser à quelqu'un mais peux répondre si il est interogé. Il permet :</para>
    /// <list type = "bullet" >
    ///     <item>
    ///     <description><c>On(string chanel,(dynamic client,Message arg) => bool)</c> qui permet de définir un chanel et son callback.</description>
    ///     </item>
    ///     <item>
    ///     <description><c>DeleteOn(string chanel)</c> qui permet de détruire un chanel et son callback.</description>
    ///     </item>
    /// </list>
    /// </item>
    /// <item>
    /// <description>Serveur P2P.</description>
    /// <para>Son rôle est de se peer "link" avec un autre SocketRouter afin de s'adresser avec lui. Il n'écoute pas mais peux evoyer un message sur une IP spécifique. Il permet :</para>
    /// <list type = "bullet" >
    ///     <item>
    ///     <description><c>Subscribe(string? ipv6)</c> qui permet au Serveur P2P de se peer avec un autre routeur.</description>
    ///     </item>
    ///     <item>
    ///     <description><c>Send(string message)</c> qui permet d'adresse un message général au routeur peer.</description>
    ///     </item>
    ///     <item>
    ///     <description><c>SendOn(string message , string chanel)</c> qui permet d'adresser un message sur le chanel d'un routeur peer.</description>
    ///     </item>
    ///     <item>
    ///     <description><c>SendTo(string ip , string message)</c> qui permet d'adresser un message général vers l'ip routeur.</description>
    ///     </item>
    ///     <item>
    ///     <description><c>SendToOn(string ip , string message , string chanel)</c> qui permet d'adresser un message vers le chanel d'un ip routeur.</description>
    ///     </item>
    /// </list>
    /// </item>
    /// </list>
    /// <para>Par concatenation des serveices SocketRouter permet :</para>
    /// <list type = "bullet" >
    ///     <item>
    ///     <description><c>Subscribers()</c> qui retourne une List[IPAddress] de tout les utilisateurs ayant fait une demande de peer à Server Listener.</description>
    ///     </item>
    ///     <item>
    ///     <description><c>Emit(string message)</c> qui permet d'émettre un message à toute les adresses de routeur ayant fais une demande de peer (subscribe) avec celui-ci.</description>
    ///     </item>
    ///     <item>
    ///     <description><c>EmitOn(string chanel , string message)</c> qui permet d'émettre un message sur un chanel de toute les adresses de routeur ayant fais une demande de peer (subscribe) avec celui-ci.</description>
    ///     </item>
    /// </list>
    /// <para>La librairie <c>Protocoles</c> integrée permet de : </para>
    /// <list type = "bullet" >
    ///     <item>
    ///     <description><c>GetDisconectedIP()</c> Retourne un tableau contenant tout les ip non disponible.</description>
    ///     </item>
    ///     <item>
    ///     <description><c>Ping()</c> Retourne PingAnalitics. <c>PingAnalitics.users</c> contient une liste de ping basée sur les subscribers [{ ip <c>IPAddress</c> , ping <c>double</c> }].</description>
    ///     </item>
    /// </list>
    /// </summary>
    public class SocketRouter
    {

        private SocketListener _server;
        private SocketP2P _sender;
        private Protocoles protocoles = new Protocoles();

        public SocketRouter(int serverPort = 11111, int emitorPort = 11111)
        {
            this._server = new SocketListener(serverPort, this);
            this._sender = new SocketP2P(emitorPort, this);
        }

        /// <summary>
        /// Envois d'un message vers notre peer.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public SocketP2P Send(string message)
        {
            return this._sender.Send(message);
        }

        /// <summary>
        /// Envois d'un message sur le chanel de notre peer.
        /// </summary>
        /// <param name="chanel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public SocketP2P SendOn(string chanel, string message = "")
        {
            return this._sender.SendOn(chanel, message);
        }

        /// <summary>
        /// Envois d'un message vers un IP.
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public SocketP2P SendTo(string IP, string message)
        {
            return this._sender.SendTo(IP, message);
        }

        /// <summary>
        /// Envois d'un message sur le chanel d'un IP.
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="chanel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public SocketP2P SendToOn(string IP, string chanel, string message)
        {
            return this._sender.SendToOn(IP, chanel, message);
        }

        /// <summary>
        /// Retourne une List<IPAddress> de tout les utilisateurs ayant fait une demande de peer à Server Listener
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="chanel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<IPAddress> Subribers()
        {
            return this._server.subscriptions.Subribers();
        }

        /// <summary>
        /// Retourne PingAnalitics des subscribers à Server Listener.
        /// </summary>
        /// <returns></returns>
        public PingAnalitics Ping()
        {
            return this.protocoles.Ping(this._server.subscriptions.Subribers(), this._sender);
        }

        /// <summary>
        /// Retourne un tableau contenant les IP n'étant pas connecter
        /// </summary>
        /// <returns></returns>
        public List<IPAddress> GetDisconectedIp()
        {
            return this.protocoles.GetDisconectedIP(this._server.subscriptions.Subribers(), this._sender);
        }

        /// <summary>
        /// Emission d'un message à tout peer souscris.
        /// </summary>
        /// <param name="message"></param>
        public void Emit(string message, Action<dynamic, Message> callback = null)
        {
            List<IPAddress> addressList = this._server.subscriptions.Subribers();
            foreach (IPAddress ip in addressList)
            {
                SocketP2P sender = this.SendTo(ip.ToString(), message);
                if (callback != null) sender.OnReply(callback);
            }
        }

        /// <summary>
        /// Emission d'un message sur le chanel de tout peer souscris.
        /// </summary>
        /// <param name="chanel"></param>
        /// <param name="message"></param>
        public void EmitOn(string chanel, string message, Action<dynamic, Message> callback = null)
        {
            try
            {
                List<IPAddress> addressList = this._server.subscriptions.Subribers();
                foreach (IPAddress ip in addressList)
                {
                    string ipToSend = ip.ToString().Remove(ip.ToString().IndexOf('%'));
                    SocketP2P sender = this.SendToOn(ipToSend, chanel, message);
                    if (callback != null) sender.OnReply(callback);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Démare le Routeur.
        /// </summary>
        /// <returns></returns>
        public SocketRouter Start()
        {
            this._server.Start();
            return this;
        }

        /// <summary>
        /// Peer du routeur vers l'ip d'un autre routeur.
        /// </summary>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public SocketRouter Subscribe(string? ipv6 = null)
        {
            this._sender.Subscribe(ipv6);
            return this;
        }

        public bool IsIpOnLine(string ip)
        {
            return this._sender.IsIpOnLine(System.Net.IPAddress.Parse(ip));
        }

        /// <summary>
        /// Print des informations du routeur dans la console
        /// </summary>
        public void Infos()
        {
            this.InfoServer();
            this.InfoSender();
        }

        /// <summary>
        /// Print des information du serveur listener
        /// </summary>
        public void InfoServer()
        {
            this._server.Infos();
        }

        /// <summary>
        /// Print des information du serveur P2P
        /// </summary>
        public void InfoSender()
        {
            this._sender.Infos();
        }

        /// <summary>
        /// Routeur attribue à serveur listener un chanel et son callback.
        /// </summary>
        /// <param name="chanel"></param>
        /// <param name="callback"></param>
        public void On(string chanel, Func<dynamic, Message, bool> callback)
        {
            this._server.On(chanel, callback);
        }

        /// <summary>
        /// Routeur détruit à serveur listener un chanel et son callback.
        /// </summary>
        /// <param name="chanel"></param>
        public void DeleteOn(string chanel)
        {
            this._server.Chanels().Delete(chanel);
        }

    }
}
