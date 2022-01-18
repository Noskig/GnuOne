namespace GnuOne.Data.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public bool hasBeenRead { get; set; } = false;
        public string messageType { get; set; }
        public string info { get; set; }
        public string mail { get; set; }
        public int counter { get; set; }

        public Notification()
        {

        }
        public Notification(string _messageType, string _mail, string info)
        {
            this.messageType = _messageType;
            this.mail = _mail;
            this.info = info;   
        }
    }
}


