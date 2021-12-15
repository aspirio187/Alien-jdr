using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Alien.Socket.Models
{
    public class SocketP2P
    {

        private SocketRouter Router;

        private string ipv6;
        private int _hostPort;
        private IPHostEntry ipHost;

        private IPAddress ipAddr;
        private IPEndPoint localEndPoint;
        private System.Net.Sockets.Socket sender;

        private bool isConnected = false;

        private Message _reply = new Message("");

        private SocketChanels chanels;

        public SocketP2P(int port, SocketRouter Router)
        {
            this.Router = Router;
            this._hostPort = port;
            this.chanels = new SocketChanels(this);
        }

        public IPAddress IP()
        {
            return this.ipAddr;
        }

        public SocketP2P Subscribe(string? ipv6 = null)
        {
            this.ipv6 = ipv6;
            return this.SendOn("subscribe", Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString());
        }


        /*
         * @{name}      Infos
         * @{type}      public void
         * @{desc}      
         */
        public void Infos()
        {
            Debug.WriteLine((this.ipAddr == null ? $"socketEmitor is linked with unscribe:{11111}" : $"socketEmitor is linked with {this.ipAddr}:{11111}"));
        }

        public bool IsIpOnLine(IPAddress IP)
        {
            System.Net.Sockets.Socket sender = new System.Net.Sockets.Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                IPEndPoint endPoint = new IPEndPoint(IP, 11111);
                sender.Connect(endPoint);
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                return true;
            }
            catch (ArgumentNullException ane)
            {
                this._OnError("ArgumentNullException", ane);
                return false;
            }

            catch (SocketException se)
            {
                this._OnError("SocketException", se);
                return false;
            }

            catch (Exception e)
            {
                this._OnError("Exception", e);
                return false;
            }
        }

        /*
         * @{name}      Connect
         * @{type}      public void
         * @{desc}      
         */
        public void Connect()
        {
            this._connect(this.localEndPoint, this.sender);
        }

        /*
         * @{name}      SendOn
         * @{type}      public void
         * @{desc}      
         */
        public SocketP2P SendOn(string chanelName, string message)
        {
            var msg = new
            {
                eventTime = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString(),
                chanel = chanelName,
                data = message
            };

            return this.Send(JsonConvert.SerializeObject(msg));

            //return this.Send($"{{" +
            //    $"\"eventTime\" : \"{(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()}\"," +
            //    $"\"chanel\" : \"{chanelName}\"," +
            //    $"\"data\" : \"{message}\"" +
            //    $"}}");
        }

        /*
         * @{name}      Send
         * @{type}      public void
         * @{desc}      
         */
        public SocketP2P Send(string message)
        {
            this._setEndpoint();
            this._setSender();
            this._connect(this.localEndPoint, this.sender);
            this._execClient(this.sender, message);
            this._close(this.sender);
            return this;
        }

        public SocketP2P SendToOn(string IP, string chanelName, string message)
        {
            return this.SendTo(IP, $"{{" +
                $"\"eventTime\" : \"{(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()}\"," +
                $"\"chanel\" : \"{chanelName}\"," +
                $"\"data\" : \"{message}\"" +
                $"}}");
        }

        public SocketP2P SendTo(string IP, string message)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), 11111);
            System.Net.Sockets.Socket sender = new System.Net.Sockets.Socket(this.ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this._connect(endPoint, sender);
            this._execClient(sender, message);
            this._close(sender);
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
                this._close(this.sender);
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
        private void _connect(IPEndPoint endpoint, System.Net.Sockets.Socket sender)
        {
            try
            {
                sender.Connect(endpoint);
                this._print("Socket connected to : " + sender.RemoteEndPoint.ToString()); // Nous imprimons des informations EndPoint à laquel nous sommes connectés
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
        private void _send(System.Net.Sockets.Socket sender, string message)
        {
            int byteSent = sender.Send(this._normalize(message));
        }

        private string _Onmessage(System.Net.Sockets.Socket sender)
        {
            // Data buffer
            byte[] messageReceived = new byte[1024];
            int byteRecv = sender.Receive(messageReceived);
            return Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
        }

        private void _print(string message)
        {
            Debug.Write($"{this.ipHost.HostName} ");
            Debug.Write(message);
            Debug.WriteLine("");
        }

        /*
         * @{name}      _execClient
         * @{type}      private void
         * @{desc}      Établissez le point de terminaison distant pour le socket. Cet exemple utilise le port 11111 sur l’ordinateur local.
         *              Modifie en interne les valeurs de ipHost , ipAddr , localEndPoint
         */
        private void _execClient(System.Net.Sockets.Socket sender, string message)
        {
            try
            {
                if (this.isConnected == false) throw new InvalidCastException("Socket n'est pas connecter au point d'accès.");
                this._send(sender, message);
                this._reply = new Message(this._Onmessage(sender));
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
        private void _close(System.Net.Sockets.Socket sender)
        {
            try
            {
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
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
            Debug.Write($"{this.ipHost.HostName}-");
            Debug.Write($"{type} ");
            Debug.Write($"{error.StackTrace.Split("\\")[error.StackTrace.Split("\\").Length - 1].Split(":line")[0]}:");
            Debug.Write($"{error.StackTrace.Split(":line ")[error.StackTrace.Split(":line ").Length - 1]} ");
            Debug.Write($"{error.Message}");
            Debug.WriteLine("");
        }

    }
}
