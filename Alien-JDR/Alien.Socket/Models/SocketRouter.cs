using System;
using System.Collections.Generic;
using System.Text;

namespace Alien.Socket.Models
{
    public class SocketRouter
    {
        private SocketCaptor _server;
        private SocketEmitor _sender;

        public SocketRouter(int serverPort = 11111, int emitorPort = 11111)
        {
            this._server = new SocketCaptor(serverPort);
            this._sender = new SocketEmitor(emitorPort);
        }

        /*
        * @{name}      Send
        * @{type}      public IPAddress
        */
        public SocketEmitor Send(string message)
        {
            return this._sender.Send(message);
        }

        /*
        * @{name}      Send
        * @{type}      public IPAddress
        */
        public SocketEmitor SendOn(string chanel, string message = "")
        {
            return this._sender.SendOn(chanel, message);
        }

        /*
        * @{name}      Send
        * @{type}      public IPAddress
        */
        public void Emit(string message)
        {
            this._server.Emit(message);
        }

        /*
        * @{name}      Send
        * @{type}      public IPAddress
        */
        public void EmitOn(string chanel, string message)
        {
            this._server.EmitOn(chanel, message);
        }

        /*
        * @{name}      StartServer
        * @{type}      public IPAddress
        */
        public SocketRouter Start()
        {
            this._server.Start();
            return this;
        }

        /*
        * @{name}      Subscribe
        * @{type}      public IPAddress
        */
        public SocketRouter Subscribe(string? ipv6 = null)
        {
            this._sender.Subscribe(ipv6);
            return this;
        }

        public void Infos()
        {
            this.InfoServer();
            this.InfoSender();
        }

        /*
        * @{name}      InfoServer
        * @{type}      public IPAddress
        */
        public void InfoServer()
        {
            this._server.Infos();
        }

        /*
        * @{name}      InfoSender
        * @{type}      public IPAddress
        */
        public void InfoSender()
        {
            this._sender.Infos();
        }

        /*
        * @{name}      Subscribe
        * @{type}      public IPAddress
        */
        public void On(string chanel, Func<dynamic, Message, bool> callback)
        {
            this._server.On(chanel, callback);
        }

        /*
        * @{name}      Subscribe
        * @{type}      public IPAddress
        */
        public void DeleteOn(string chanel)
        {
            this._server.Chanels().Delete(chanel);
        }
    }
}
