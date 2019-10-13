using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Url_Shortner.Controllers;
using Url_Shortner.DataTransferObject;
using Url_Shortner.Models;

namespace Url_Shortner.Controllers
{
    public class MyUrlController : Controller
    {
        // GET: UrlSearch
        public ActionResult Index(string shortUrl)
        
        {
            UrlController urlController = new UrlController();
            var longUrl = urlController.getLongUrl(shortUrl);

            if (longUrl is null)
            {
                ViewBag.Message = "No such Short Url Exist";
                return View("Error");

                
            }
            else
            {
               return  Redirect(longUrl);
            }
        }

        public ActionResult CreateShortUrl()
        {
            
                return View("CreateShortUrl");


           
        }

        [HttpPost]
        public ActionResult CreateShortUrl(string longUrl)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:57329/api/");

                client.BaseAddress = new Uri("https://url-shortner-dev-as.azurewebsites.net/api/");

            
                //HTTP POST
                var postTask = client.PostAsJsonAsync("shortme?url=" + longUrl,longUrl);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseData = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    if (!string.IsNullOrWhiteSpace(responseData))
                    {
                        var responseDataObject =
                            JsonConvert.DeserializeObject<UrlPair>(responseData);

                        return View(responseDataObject);
                    }
                    else
                        return View("Error");



                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            
            return View("Error");
        }
    }
}