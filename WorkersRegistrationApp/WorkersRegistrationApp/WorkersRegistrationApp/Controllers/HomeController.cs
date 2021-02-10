using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkersRegistrationApp.Controllers
{
    /// <summary>
    /// The <c>HomeController</c> class.
    /// Contains method for passing start page to the View
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }
       
    }
}