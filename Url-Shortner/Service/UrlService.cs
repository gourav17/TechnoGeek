using System;
using System.Linq;
using System.Text;
using Url_Shortner.DataTransferObject;
using Url_Shortner.Models;

namespace Url_Shortner.Service
{

    public class UrlService : Service
    {
        private static  String ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static  int BASE = ALPHABET.Length;

        public UrlPairDto makeShort(string longUrl)
        {
            #region contracts
            if (longUrl == null)
            {
                throw new ArgumentException("longUrl cannot be null");
            }

            #endregion

            UrlPair urlData = new UrlPair();

            #region Generate a new code first
           
         
            urlData.shortURL = NewUniqueCode();

            #endregion
            urlData.longURL = longUrl;
           
            urlData.DateCreate = DateTime.UtcNow;

            UrlPairDto newUrlDto = Create(urlData);
            return newUrlDto;
        }

        public string NewUniqueCode()
        {
            Random rnd = new Random();
            int num = rnd.GetHashCode();

            StringBuilder sb = new StringBuilder();

            while (num > 0)
            {
                sb.Append(ALPHABET[(num % BASE)]);
                num /= BASE;
            }
            sb.ToString().Reverse();
            StringBuilder builder = new StringBuilder();
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                builder.Append(sb[i]);
            }
            return builder.ToString();

          
        }

        #region Create
        public UrlPairDto Create(UrlPair urlData)
        {
            #region contracts
            if (urlData == null)
            {
                throw new ArgumentException("urlData cannot be null");
            }

            #endregion
           
            urlDbContext.UrlTable.Add(urlData);
            urlDbContext.SaveChanges();

            return urlData.ToUrlDto();
        }
        #endregion

        #region Read
        public UrlPair Read(string shortUrl)
        {
            if (shortUrl == default(string))
            {
                throw new ArgumentNullException(nameof(shortUrl));
            }

            UrlPair urlData = urlDbContext.UrlTable.Where(x => x.shortURL == shortUrl).FirstOrDefault();

            return urlData;
        }
        #endregion

        #region Delete
        public void Delete(UrlPair urlData)
        {
            urlDbContext.UrlTable.Attach(urlData);
            urlDbContext.UrlTable.Remove(urlData);
           
            urlDbContext.SaveChanges();
        }
        #endregion

    }
}