using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Url_Shortner.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class UrlPair
    {
        [Key]
        public int urlID { get; set; }
               
        public string shortURL { get; set; }

        public string longURL { get; set; }
        
    }
}