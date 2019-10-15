using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using Url_Shortner.DataTransferObject;
using Url_Shortner.Models;
using Url_Shortner.Service;

namespace Url_Shortner.Controllers
{
    // [ControllerName("Client")]
    //  [ApiVersion("1.0")]


    public partial class UrlController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/shortme", Name = "CreateshortUrl")]

        public IHttpActionResult Post(string url)
        {
            #region contracts
            if (!Regex.IsMatch(url, ""))
            {
                return BadRequest("url is not in correct format. ");
            }
            #endregion

          
            UrlService urlService = new UrlService();


            {
                UrlPair resultRecord = urlService.ReadbyHash(url);

                HttpResponseMessage response = new HttpResponseMessage();
                if (resultRecord != null)
                {
                    UrlPairDto recordUrl = resultRecord.ToUrlDto();
                    response = Request.CreateResponse(HttpStatusCode.Created, recordUrl);
                    return ResponseMessage(response);

                }
                UrlPairDto shortUrl = urlService.makeShort(url);
                response = Request.CreateResponse(HttpStatusCode.Created, shortUrl);
                return ResponseMessage(response);
            }
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/getlongurl", Name = "GetlongUrl")]
        public IHttpActionResult Get(string url)
        {
            #region contracts
            if (!Regex.IsMatch(url, ""))
            {
                return BadRequest("url is not in correct format. ");
            }
            #endregion

            UrlService urlService = new UrlService();
            {

                UrlPairDto urlPairDto = urlService.Read(url).ToUrlDto();


                return Ok(urlPairDto);
            }
        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/redirecttolongurl", Name = "RedirecttolongUrl")]
        public IHttpActionResult Redirectme(string url)
        {
            #region contracts
            if (!Regex.IsMatch(url, ""))
            {
                return BadRequest("url is not in correct format. ");
            }
            #endregion

            UrlService urlService = new UrlService();
            {

                UrlPairDto urlDataDto = urlService.Read(url).ToUrlDto();


                if (urlDataDto is null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");


                    return ResponseMessage(response);
                }
                else
                {
                    return Redirect(urlDataDto.longURL);
                }
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/removeurl", Name = "removeUrl")]
        public IHttpActionResult RemoveUrl(string url)
        {
            #region contracts
            if (!Regex.IsMatch(url, ""))
            {
                return BadRequest("url is not in correct format. ");
            }
            #endregion

            UrlService urlService = new UrlService();
            {

                UrlPair urlPair = urlService.Read(url);

                if (urlPair is null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");


                    return ResponseMessage(response);
                }
                else
                {
                    urlService.Delete(urlPair);
                }

                return Ok(urlPair);
            }
        }

        public string getLongUrl(string shortUrl)
        {
            #region contracts
            if (!Regex.IsMatch(shortUrl, ""))
            {
                return "url is not in correct format. ";
            }
            #endregion

            UrlService urlService = new UrlService();
            {

                UrlPairDto urlPairDto = urlService.Read(shortUrl).ToUrlDto();


                return urlPairDto?.longURL;
            }
        }


    }
}
