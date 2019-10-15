using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Url_Shortner.DataTransferObject;
using Url_Shortner.Models;

namespace Url_Shortner.Service
{

    public class UrlService : Service
    {
        private static string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly int BASE = ALPHABET.Length;

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

            urlData.urlHash = shortHash(longUrl);
            urlData.shortURL = NewUniqueCode(urlData.urlHash);
            #endregion

            urlData.longURL = longUrl;

            urlData.DateCreate = DateTime.UtcNow;


            UrlPairDto newUrlDto = Create(urlData);
            return newUrlDto;
        }

        public int shortHash(string longUrl)
        {
            //   Random rnd = new Random();
            // int num = rnd.GetHashCode();

            string hash = CalculateMD5Hash(longUrl);
            string shortHash = hash.Substring(0, 7);

            int num = int.Parse(shortHash, System.Globalization.NumberStyles.HexNumber);
            return num;
        }
        public string NewUniqueCode(int num)
        {
            //   Random rnd = new Random();
            // int num = rnd.GetHashCode();

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


        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
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

        #region Read UrlHash
        public UrlPair ReadbyHash(string longUrl)
        {
            if (longUrl == default(string))
            {
                throw new ArgumentNullException(nameof(longUrl));
            }

            int num = shortHash(longUrl);

            UrlPair urlData = urlDbContext.UrlTable.Where(x => x.urlHash == num).FirstOrDefault();

            return urlData;
        }
        #endregion

    }
}