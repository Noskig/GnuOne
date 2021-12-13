using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class User
    {
        [Key]
        public int userid { get; set; }
        public string username { get; set; }
        public string Email { get; set; } 

        public User()
        {

        }
    }

}
