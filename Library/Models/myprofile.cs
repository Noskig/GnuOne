using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class myprofile
    { 
    
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string myUserInfo { get; set; }
        public int pictureID { get; set; }
        public standardpictures standardpictures { get; set; } 
        public int? tagOne { get; set; }
        public int? tagTwo { get; set; }
        public int? tagThree { get; set; }

    }
}
