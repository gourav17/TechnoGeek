using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Url_Shortner.Service;

namespace Url_Shortner.Controllers
{
   // [ControllerName("Client")]
  //  [ApiVersion("1.0")]


    public partial class UrlController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/shortme/{Url}", Name = "GetshortUrl")]
       
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
                var shortUrl = urlService.makeShort();
                //var urlModel = url.ToUrlDto();
             
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Redirect, shortUrl);
                return Ok(response);
            }
        }


    }
}
