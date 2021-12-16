using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Alien.Socket.Models
{

    class SocketListener
    {
        private SocketRouter Router;
        private int port;                   //
        private IPHostEntry ipHost;         //
        private IPAddress ipAddr;           //
        private IPEndPoint localEndPoint;   //
        private System.Net.Sockets.Socket listener;            //

        private Task serverTask;            // Threading Task du serveur, pour qu'il ne bloque pas le processus
        private System.Net.Sockets.Socket clientSocket;        //

        private int nbrUsers;               // nombre d'utilisateur max présent dans la liste Socket.Listen()

        private SocketChanels chanels;

        public SocketSubscription subscriptions = new SocketSubscription();

        ////////////////////////
        /////////PUBLIC/////////
        ////////////////////////

        /* @{name}      socketEmitor
         * @{type}      public constructor
         * @{desc}      Constructeur de la class socketEmitor
         * @{params}
         *      int? {nbrUsers} peut être null . SI null, par défaut 10
         *      int? {port}     peut être null . SI null, par défaut 11111
         */
        public SocketListener(int port, SocketRouter Router)
        {
            this.nbrUsers = 10;
            this.port = (port != null ? (int)port : 11111);
            this.chanels = new SocketChanels(this);
            this._setEndpoint();
            this._setListener();
            this._setHandlers();
            this.serverTask = new Task(this._execServer);
        }

        public SocketChanels Chanels() { return this.chanels; }

        /*
         * @{name}      Infos
         * @{type}      public void
         * @{desc}      
         */
        public void Infos()
        {
            Debug.WriteLine($"socketCaptor server is running on {this.IP()}:{this.Port()}");
        }

        /*
         * @{name}      Start
         * @{type}      public void
         * @{desc}      Permet de démarrer le serveur sur un thread.
         */
        public SocketListener Start()
        {
            this.serverTask.Start();
            return this;
        }

        /*
          * @{name}      IP
          * @{type}      public IPAddress
          */
        public IPAddress IP()
        {
            return this.ipAddr;
        }

        /*
          * @{name}      Port
          * @{type}      private void
          */
        public int Port()
        {
            return this.port;
        }

        public void Subscribe(string ipv6)
        {
            this.subscriptions.Add(ipv6);
        }

        public void Unsubscribe(string ipv6)
        {
            this.subscriptions.Remove(ipv6);
        }

        public void OnError(string? type, dynamic error)
        {
            this._OnError(type, error);
        }

        /*
         * @{name}      On
         * @{type}      public int
         */
        public void On(string chanel, Func<dynamic, Message, bool> callback)
        {
            this.chanels.Add(chanel, callback);
        }

        public bool Reply(string message)
        {
            var replyMsg = new
            {
                chanel = "reply",
                data = message
            };

            var sendMsg = JsonConvert.SerializeObject(replyMsg);
            this._send(sendMsg);

            //this._send($"{{" +
            //    $"\"chanel\" : \"reply\"," +
            //    $"\"data\" : \"{message}\"" +
            //    $"}}");
            this._close();
            return true;
        }

        public void Emit(string message)
        {
            /*this.subscriptions.SendToAll(this, message);*/
        }

        public void EmitOn(string chanel, string message)
        {
            /*this.subscriptions.SendToAllOn(this, chanel, message);*/
        }

        ////////////////////////
        /////////Private////////
        ////////////////////////

        /*
         * @{name}      _setEndpoint
         * @{type}      private void
         * @{desc}      Définition du "endpoint" de l'émeteur. Cet exemple utilise le port 11111 sur l’ordinateur local.
         *              Modifie en interne les valeurs de ipHost , ipAddr , localEndPoint
         */
        private void _setEndpoint()
        {
            this.ipHost = Dns.GetHostEntry(Dns.GetHostName());
            this.ipAddr = ipHost.AddressList[0];
            this.localEndPoint = new IPEndPoint(ipAddr, this.port);
        }

        /*
         * @{name}      _setListener
         * @{type}      private void
         */
        private void _setListener()
        {
            this.listener = new System.Net.Sockets.Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        private void _setHandlers()
        {
            Func<dynamic, Message, bool> Subscribe = (dynamic cli, Message arg) =>
            {
                Debug.Write($"{this.ipHost.HostName}-");
                Debug.Write((arg.chanel == null ? $"general" : $"{arg.chanel}"));
                Debug.Write($" : {arg.message}");
                Debug.WriteLine("");

                cli.Subscribe(arg.message);
                return cli.Reply("Souscris");
            };

            Func<dynamic, Message, bool> Unsubscribe = (dynamic cli, Message arg) =>
            {
                Debug.WriteLine($"Delete d'abonement {arg.message}");
                cli.Unsubscribe(arg.message);
                return cli.Reply("Désouscris");
            };

            Func<dynamic, Message, bool> Ping = (dynamic cli, Message arg) =>
            {
                string messageBytes = Encoding.UTF8.GetByteCount(arg.message).ToString();
                double delay = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds - arg.eventTime;
                /*                this._print(new Message($"ping - {messageBytes} byte recive in {delay} ms"));*/
                return cli.Reply(delay.ToString());
            };

            this.On("subscribe", Subscribe);
            this.On("unsubscribe", Unsubscribe);
            this.On("ping", Ping);
        }

        /*
         * @{name}      _send
         * @{type}      private void
         * @{desc}      Envois d'un message au client à l’aide de la méthode Send()
         */
        private void _send(string message)
        {
            this.clientSocket.Send(Encoding.ASCII.GetBytes(message));
        }

        /*
         * @{name}      _result
         * @{type}      private void
         * @{desc}      WriteLine du résultat de la donnée reçue
         */
        private Message _Onmessage()
        {
            // Data buffer
            byte[] bytes = new Byte[1024];
            string data = null;

            while (true)
            {

                int numByte = this.clientSocket.Receive(bytes);

                data += Encoding.ASCII.GetString(bytes, 0, numByte);

                if (data.IndexOf("<EOF>") > -1)
                    break;
            }

            return new Message(data.Split("<EOF>")[0]);
        }

        /*
         * @{name}      _print
         * @{type}      private void
         * @{desc}      Affichage en bleu d'un message dans la console.
         */
        private async void _print(Message data)
        {
            Debug.Write($"{this.ipHost.HostName}-");
            Debug.Write((data.chanel == null ? $"general" : $"{data.chanel}"));
            Debug.Write($" : {data.message}");
            Debug.WriteLine("");
            if (data.chanel != null) this.chanels.Exec(data);
            else
            {
                this._send("true");
                this._close();
            }
        }

        /*
         * @{name}      _execServer
         * @{type}      private void
         */
        private void _execServer()
        {
            try
            {
                // En utilisant la méthode Bind(), nous associons une adresse réseau au socket serveur.
                // Tout le client qui se connectera à ce socket serveur doit connaître cette adresse réseau.
                this.listener.Bind(this.localEndPoint);

                // En utilisant la méthode Listen(), nous créons la liste des clients qui voudront se connecter au serveur.
                this.listener.Listen(this.nbrUsers);

                this._accept();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private void _accept()
        {
            while (true)
            {
                // Attente de la connexion entrante. À l’aide de la méthode Accept(), le serveur acceptera la connexion du client.
                this.clientSocket = this.listener.Accept();

                // Affichage du message entrant
                this._print(this._Onmessage());
            }
        }

        /*
         * @{name}      _close
         * @{type}      private void
         * @{desc}      Fermez le socket client à l’aide de la méthode Close().
         *              Après la fermeture, nous pouvons utiliser le socket fermé pour une nouvelle connexion client
         */
        private void _close()
        {
            this.clientSocket.Shutdown(SocketShutdown.Both);
            this.clientSocket.Close();
        }

        private void _OnError(string? type, dynamic error)
        {
            Debug.Write($"{this.ipHost.HostName}-");
            Debug.Write($"{type} ");
            Debug.Write($"{error.StackTrace.Split("\\")[error.StackTrace.Split("\\").Length - 1].Split(":line")[0]}:");
            Debug.Write($"{error.StackTrace.Split(":line ")[error.StackTrace.Split(":line ").Length - 1]} ");
            Debug.Write($" {error.Message}");
            Debug.WriteLine("");
        }

    }
}
