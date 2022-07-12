using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Alien.Socket.Models
{
    public class PingUser
    {
        public IPAddress ip;
        public double ping;
        public PingUser(IPAddress ip, double ping)
        {
            this.ip = ip;
            this.ping = ping;
        }
    }

    public class PingAnalitics
    {
        public List<PingUser> users { get; } = new List<PingUser>();
        public void Add(IPAddress ip, double ping)
        {
            this.users.Add(new PingUser(ip, ping));
        }
    }

    public class Protocoles
    {
        public List<IPAddress> GetDisconectedIP(List<IPAddress> ipList, SocketP2P Sender)
        {
            List<IPAddress> DisconectedIp = new List<IPAddress>();
            foreach (IPAddress ip in ipList)
            {
                if (Sender.IsIpOnLine(ip) == false) DisconectedIp.Add(ip);
            }
            return DisconectedIp;
        }

        public PingAnalitics Ping(List<IPAddress> ipList, SocketP2P sender)
        {
            PingAnalitics pingResult = new PingAnalitics();
            foreach (IPAddress ip in ipList)
            {
                sender.SendToOn(ip.ToString(), "ping", "").OnReply((dynamic cli, Message arg) =>
                {
                    pingResult.Add(ip, Convert.ToDouble(arg.message));
                });
            }
            return pingResult;
        }
    }
}
