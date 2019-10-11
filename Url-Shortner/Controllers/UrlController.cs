using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Url_Shortner.DataTransferObject;
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

                UrlPairDto shortUrl = urlService.makeShort(url);
              
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, shortUrl);
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

                UrlPairDto urlPairDto = urlService.Read(url);
           
             //   HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, urlPairDto);
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

                UrlPairDto urlDataDto = urlService.Read(url);
              
               // HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Redirect, urlDataDto.longURL);
                return Redirect(urlDataDto.longURL);
            }
        }

    }
}
