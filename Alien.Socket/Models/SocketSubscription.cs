using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Alien.Socket.Models
{
    class SocketSubscription
    {

        /*private List<SocketP2P> _subcriptions = new List<SocketP2P>();*/
        private List<IPAddress> subsribersIP = new List<IPAddress>();

        public SocketSubscription()
        {

        }

        public List<IPAddress> Subribers()
        {
            return this.subsribersIP;
        }

        public void Add(string ipv6)
        {
            /*this._subcriptions.Add(new SocketP2P(11111,ipv6));*/
            this.subsribersIP.Add(IPAddress.Parse(ipv6));
        }

        public void Remove(string subIp)
        {
            int x = _subByIpv6(subIp);
            if (x > -1)
            {
                this.subsribersIP.RemoveAt(x);
            }
        }

        private int _subByIpv6(string subIpv6)
        {
            for (int i = 0; i < this.subsribersIP.Count; i++)
            {
                if (subIpv6 == this.subsribersIP[i].ToString()) return i;
            }
            return -1;
        }
    }
}
