using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndovinaCanzoni.Messages
{
    public class Message
    {
        public string Key { get; private set; }
        public Message(string key)
        {
            Key = key;
        }
    }
}
