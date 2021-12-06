using System;
using System.Collections.Generic;
using System.Text;

namespace Alien.Socket.Models
{
    public class SocketChanel
    {
        private List<Func<dynamic, Message, bool>> callBacks = new List<Func<dynamic, Message, bool>>();
        private List<string> chanelsNames = new List<string>();
        private dynamic root;

        public SocketChanel(dynamic root)
        {
            this.root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public List<string> Chanels { get { return chanelsNames; } }

        public void Exec(Message message)
        {
            try
            {
                if ((message.chanel != null) && (this.chanelsNames.Contains(message.chanel))) _exec(message);
                else throw new Exception($"Chanel \"{message.chanel}\" n'est pas définis");
            }
            catch (Exception e)
            {
                this.root.OnError("Exception", e);
            }
        }

        public void Add(string chanelName, Func<dynamic, Message, bool> callback)
        {
            this._add(chanelName, callback);
        }

        public bool Delete(string chanelName)
        {
            int x = this._getIdFromName(chanelName);
            if (x > -1)
            {
                this.callBacks.RemoveAt(x);
                this.chanelsNames.RemoveAt(x);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void _add(string chanelName, Func<dynamic, Message, bool> callback)
        {
            if (this._isChanelExist(chanelName) == false)
            {
                this.callBacks.Add(callback);
                this.chanelsNames.Add(chanelName);
            }
        }

        private bool _isChanelExist(string chanelName)
        {
            return this.chanelsNames.Contains(chanelName);
        }

        private int _getIdFromName(string name)
        {
            for (int i = 0; i < this.chanelsNames.Count; i++)
            {
                if (this.chanelsNames[i] == name) return i;
            }
            return -1;
        }

        private Func<dynamic, Message, bool> _getCallBackFromName(string name)
        {
            for (int i = 0; i < this.chanelsNames.Count; i++)
            {
                if (this.chanelsNames[i] == name) return this.callBacks[i];
            }
            return null;
        }

        private void _exec(Message message)
        {
            this.callBacks[this._getIdFromName(message.chanel)](this.root, message);
        }
    }
}
