namespace GnuOne.Data.Models
{
    public class Message
    {
        public int? ID { get; set; }
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
