using System;
using System.Linq;
using Url_Shortner.DataTransferObject;
using Url_Shortner.Models;

namespace Url_Shortner.Service
{
    public class UrlService : Service
    {
        public UrlPairDto makeShort(string longUrl)
        {


            UrlPairDto newUrlDto = Create(longUrl);
            return newUrlDto;
        }


        #region Create
        public UrlPairDto Create(string longUrl)
        {
            #region contracts
            if (longUrl == null)
            {
                throw new ArgumentException("url cannot be null");
            }

            #endregion
            UrlPair urlData = new UrlPair();

            #region Generate a new code first
            Random rnd = new Random();
            urlData.urlID = rnd.GetHashCode(); //NewCode();
            
            #endregion
            urlData.longURL = longUrl;
            urlData.shortURL = "shorturl";
            urlData.DateCreate = DateTime.UtcNow;

            urlDbContext.UrlTable.Add(urlData);
            urlDbContext.SaveChanges();

            return urlData.ToUrlDto();
        }
        #endregion

        #region Read
        public UrlPair Read(int urlID)
        {
            if (urlID == default(int))
            {
                throw new ArgumentNullException(nameof(urlID));
            }

            UrlPair urlData = urlDbContext.UrlTable.Where(x => x.urlID == urlID).FirstOrDefault();

            return urlData;
        }
        #endregion

        #region Delete
        public void Delete(UrlPair urlData)
        {
            urlDbContext.UrlTable.Attach(urlData);
            urlDbContext.UrlTable.Remove(urlData);
            urlDbContext.UrlTable.Remove(urlData);
            urlDbContext.SaveChanges();
        }
        #endregion

    }
}