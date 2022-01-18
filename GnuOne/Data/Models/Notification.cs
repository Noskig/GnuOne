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
    }
}
