using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using PortfolioViewer.Models.Portfolio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfolioViewer.Controllers
{
    [Authorize]
    [RoutePrefix("Portfolio")]
    public class PortfolioController : Controller
    {

        CustomerPortfolioEntities cpe = new CustomerPortfolioEntities();
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public PortfolioController()
        {
            
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // GET: Portfolio
        public ActionResult Index()
        {

            ViewData.Add("customerList", cpe.GetCustomers());

            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CustomerPortfolio(Customer customer)
        {
            ViewData.Add("info", cpe.GetPortfolioInfo(customer.Customer_ID).First());
            ViewData.Add("customerID",customer.Customer_ID);

            var portfolio = cpe.GetCustomerHoldings(customer.Customer_ID);

            if (portfolio == null)
            {
                return HttpNotFound();
            }
            
            return View(portfolio);
        }

        public ActionResult PortfolioKPIS(int customerID)
        {
            PortfolioKPIS kpis = cpe.GetPortfolioKPIS(customerID).First();
            return PartialView("_PartialKPIS",kpis);
        }


        public ActionResult HoldingsDetail(int customerID, int securityID)
        {
            HoldingsDetail detail = cpe.GetHoldingsDetail(customerID,securityID).First();
            return PartialView("_PartialHoldings",detail);
        }

        public ActionResult MonthlyPerformance(int customerID)
        {
            IEnumerable<MonthlyPerformance> performance = cpe.GetMonthlyPerformance(customerID);

            return PartialView("_PartialMonthlyPerformance",performance);
        }
    }
}