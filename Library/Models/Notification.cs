namespace GnuOne.Data.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public bool hasBeenRead { get; set; } = false;
        public string messageType { get; set; }
        public string info { get; set; } = String.Empty;
        public int? infoID { get; set; }
        public string mail { get; set; }
        public int counter { get; set; } = 1;

        public Notification()
        {

        }
        public Notification(string _messageType, string _mail, string info)
        {
            this.messageType = _messageType;
            this.mail = _mail;
            this.info = info;   
        }
        public Notification(string _messageType, string _mail, string info, int infoID)
        {
            this.messageType = _messageType;
            this.mail = _mail;
            this.infoID = infoID;
            this.info = info;
        }
    }
}


