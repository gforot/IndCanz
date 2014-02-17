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
