using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanHangOnline.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Http404()
        {
            return PartialView();
        }
    }
}