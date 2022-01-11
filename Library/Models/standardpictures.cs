using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class standardpictures
    {
        [Key]
        public int pictureID { get; set; }
        public string  PictureName { get; set; }
        public string PictureSrc { get; set; }
    }
}
