using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class LastUpdate
    {
        [Key]
        public int Id { get; set; }
        public DateTime timeSet { get; set; }
        
    }
}
