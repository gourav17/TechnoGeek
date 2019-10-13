using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Url_Shortner.Controllers;

namespace Url_Shortner.Controllers
{
    public class UrlSearchController : Controller
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
    }
}