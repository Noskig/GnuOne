namespace GnuOne.Data.Models
{
    public class Message
    {
        public Guid ID { get; set; } = new Guid();
        public string messageText { get; set; }

        public DateTime Sent { get; set; }

        //public bool Read { get; set; }
        public string From { get; set; }
        public string To{ get; set; }

        public Message()
        {

        }
    }
}
