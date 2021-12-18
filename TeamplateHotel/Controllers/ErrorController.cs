using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamplateHotel.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /ErrorController /

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

    }
}
