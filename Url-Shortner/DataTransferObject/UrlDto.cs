using System;
using System.Collections.Generic;

namespace Url_Shortner.DataTransferObject
{
    public class UrlPairDto
    {
        public int urlID { get; set; }

        public int urlHash { get; set; }
        public string shortURL { get; set; }

        public string longURL { get; set; }
        public DateTime? DateCreate { get; set; }

        public bool secure
        {
            get
            {
                if (longURL.StartsWith("https"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
                      
        }
    }
}