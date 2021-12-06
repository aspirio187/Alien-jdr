using System;
using System.Collections.Generic;
using System.Text;

namespace Alien.Socket.Models
{
    class SocketSubscription
    {
        private List<SocketEmitor> _subcriptions = new List<SocketEmitor>();

        public SocketSubscription()
        {

        }

        public void Add(string ipv6)
        {
            this._subcriptions.Add(new SocketEmitor(11111, ipv6));
        }

        public void Remove(string subIp)
        {
            int x = _subByIpv6(subIp);
            if (x > -1)
            {
                this._subcriptions.RemoveAt(x);
            }
        }

        private int _subByIpv6(string subIpv6)
        {
            for (int i = 0; i < this._subcriptions.Count; i++)
            {
                if (subIpv6 == this._subcriptions[i].IP().ToString()) return i;
            }
            return -1;
        }

        public void SendToAll(SocketCaptor cli, string message)
        {
            foreach (SocketEmitor se in this._subcriptions) se.Send(message);
        }

        public void SendToAllOn(SocketCaptor cli, string chanel, string message)
        {
            foreach (SocketEmitor se in this._subcriptions) se.SendOn(chanel, message);
        }
    }
}
