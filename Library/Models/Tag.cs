using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Tag
    {
        [Key]
        public int ID { get; set; }
        public string tagName { get; set; }
    }
}
