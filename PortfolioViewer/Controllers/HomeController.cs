using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PortfolioViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfolioViewer.Controllers
{
    public class HomeController : Controller
    {   
        [Authorize]
        public ActionResult Index()
        {
            var userMgr = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userMgr.FindById(User.Identity.GetUserId());

            ViewData["user"] = user;
            return View();
        }

    }
}