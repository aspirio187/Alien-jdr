using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Alien.Socket.Models
{
    public class SocketEmitor
    {
        private string ipv6;
        private int _hostPort;
        private IPHostEntry ipHost;

        private IPAddress ipAddr;
        private IPEndPoint localEndPoint;
        private System.Net.Sockets.Socket sender;

        private bool isConnected = false;

        private Message _reply = new Message("");

        private SocketChanel chanels;

        public SocketEmitor(int port, string? ipv6 = null)
        {
            this._hostPort = port;
            this.ipv6 = ipv6;
            this.chanels = new SocketChanel(this);
        }

        public IPAddress IP()
        {
            return this.ipAddr;
        }

        public SocketEmitor Subscribe(string? ipv6 = null)
        {
            this.ipv6 = ipv6;
            this.SendOn("subscribe", Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString());
            return this;
        }

        /*
         * @{name}      Infos
         * @{type}      public void
         * @{desc}      
         */
        public void Infos()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine((this.ipAddr == null ? $"socketEmitor is linked with unscribe:{11111}" : $"socketEmitor is linked with {this.ipAddr}:{11111}"));
            Console.ResetColor();
        }

        /*
         * @{name}      Connect
         * @{type}      public void
         * @{desc}      
         */
        public void Connect()
        {
            this._connect();
        }

        /*
         * @{name}      SendOn
         * @{type}      public void
         * @{desc}      
         */
        public SocketEmitor SendOn(string chanelName, string message)
        {
            return this.Send($"{{" +
                $"\"eventTime\" : \"{(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()}\"," +
                $"\"chanel\" : \"{chanelName}\"," +
                $"\"data\" : \"{message}\"" +
                $"}}");
        }

        /*
         * @{name}      Send
         * @{type}      public void
         * @{desc}      
         */
        public SocketEmitor Send(string message)
        {
            this._setEndpoint();
            this._setSender();
            this._connect();
            this._execClient(message);
            this._close();
            return this;
        }

        public void Print(string message)
        {
            this._print(message);
        }

        public void OnReply(Action<dynamic, Message> callback)
        {
            callback(this, this._reply);
        }

        /*
         * @{name}      Close
         * @{type}      public void
         * @{desc}      
         */
        public void Close()
        {
            try
            {
                if (this.isConnected == false) throw new InvalidCastException("Socket n'est pas connecter au point d'accès.");
                this._close();
            }
            catch (InvalidCastException e)
            {
                this._OnError("InvalidCastException", e);
            }
        }

        public void OnError(string? type, dynamic error)
        {
            this._OnError(type, error);
        }

        /*
         * @{name}      _normalize
         * @{type}      private byte[]
         * @{return}    byte[]
         * @{desc}      Processus de normalisation d'un string en tableau de byte
         */
        private byte[] _normalize(string message)
        {
            return Encoding.ASCII.GetBytes(message + "<EOF>");
        }

        /*
         * @{name}      _setEndpoint
         * @{type}      private void
         * @{desc}      Établissez le point de terminaison distant pour le socket. Cet exemple utilise le port 11111 sur l’ordinateur local.
         *              Modifie en interne les valeurs de ipHost , ipAddr , localEndPoint
         */
        private void _setEndpoint()
        {
            this._reply = new Message("");
            this.ipHost = Dns.GetHostEntry(Dns.GetHostName());
            this.ipAddr = (this.ipv6 == null ? ipHost.AddressList[0] : IPAddress.Parse(this.ipv6));
            this.localEndPoint = new IPEndPoint(this.ipAddr, 11111);
        }

        /*
         * @{name}      _setListener
         * @{type}      private void
         * @{desc}      Création d’un socket TCP/IP à l’aide du constructeur de la classe socket.
         */
        private void _setSender()
        {
            this.sender = new System.Net.Sockets.Socket(this.ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /*
         * @{name}      _connect
         * @{type}      private void
         * @{desc}      Connection du Socket au point de terminaison distant à l’aide de la méthode Connect()
         */
        private void _connect()
        {
            try
            {
                this.sender.Connect(this.localEndPoint);
                this._print("Socket connected to : " + this.sender.RemoteEndPoint.ToString()); // Nous imprimons des informations EndPoint à laquel nous sommes connectés
                this._switchConnectionState();
            }
            catch (ArgumentNullException ane)
            {
                this._OnError("ArgumentNullException", ane);
            }

            catch (SocketException se)
            {
                this._OnError("SocketException", se);
            }

            catch (Exception e)
            {
                this._OnError("Exception", e);
            }
        }

        /*
         * @{name}      Send
         * @{type}      private void
         * @{desc}      Envois d'un message au serveur
         */
        private void _send(string message)
        {
            int byteSent = this.sender.Send(this._normalize(message));
        }

        private string _Onmessage()
        {
            // Data buffer
            byte[] messageReceived = new byte[1024];
            int byteRecv = this.sender.Receive(messageReceived);
            return Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
        }

        private void _print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{this.ipHost.HostName} ");
            Console.ResetColor();
            Console.Write(message);
            Console.WriteLine("");
        }

        /*
         * @{name}      _execClient
         * @{type}      private void
         * @{desc}      Établissez le point de terminaison distant pour le socket. Cet exemple utilise le port 11111 sur l’ordinateur local.
         *              Modifie en interne les valeurs de ipHost , ipAddr , localEndPoint
         */
        private void _execClient(string message)
        {
            try
            {
                if (this.isConnected == false) throw new InvalidCastException("Socket n'est pas connecter au point d'accès.");
                this._send(message);
                this._reply = new Message(this._Onmessage());
                this._print(this._reply.message);
            }
            catch (InvalidCastException e)
            {
                this._OnError("InvalidCastException", e);
            }
        }

        /*
         * @{name}      _close
         * @{type}      private void
         * @{desc}      Close Socket à l’aide de la méthode Close()
         */
        private void _close()
        {
            try
            {
                this.sender.Shutdown(SocketShutdown.Both);
                this.sender.Close();
                this._switchConnectionState();
            }
            catch (SocketException e)
            {
                this._OnError("SocketException", e);
            }
        }

        private void _switchConnectionState()
        {
            this.isConnected = (this.isConnected == false ? true : false);
        }

        private void _OnError(string? type, dynamic error)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{this.ipHost.HostName}-");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{type} ");
            Console.Write($"{error.StackTrace.Split("\\")[error.StackTrace.Split("\\").Length - 1].Split(":line")[0]}:");
            Console.Write($"{error.StackTrace.Split(":line ")[error.StackTrace.Split(":line ").Length - 1]} ");
            Console.ResetColor();
            Console.Write($"{error.Message}");
            Console.WriteLine("");
        }
    }
}
