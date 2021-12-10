using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Disussion
    {
        [Key]
        public int? discussionid { get; set; }
        public string headline { get; set; } 
        public string discussiontext { get; set; } 
        public string user { get; set; } 
        public DateTime createddate { get; set; }


    }
}
