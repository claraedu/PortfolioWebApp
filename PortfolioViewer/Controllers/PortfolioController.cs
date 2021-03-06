﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using PortfolioViewer.Interfaces;
using PortfolioViewer.Models.Portfolio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PortfolioViewer.Controllers
{
    [Authorize]
    [RoutePrefix("Portfolio")]
    public class PortfolioController : Controller
    {

        IPortfolioRepository _PortfolioRepository;

        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public PortfolioController(IPortfolioRepository portfolioRepository)
        {
            _PortfolioRepository = portfolioRepository;
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

            var user = UserManager.FindById(User.Identity.GetUserId());

            ViewData.Add("portfolios", _PortfolioRepository.GetPortfolios(user.CustomerID));

            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CustomerPortfolio(Portfolio p)
        {

			var user = UserManager.FindById(User.Identity.GetUserId());

			//AUTHORIZE USER
			if (!_PortfolioRepository.HasPortfolioPermissions(user.Id, p.Portfolio_ID))
			{
				return HttpNotFound();
			}

            ViewData.Add("info", _PortfolioRepository.GetPortfolioInfo(p.Portfolio_ID));
            ViewData.Add("portfolioID",p.Portfolio_ID);

            var portfolio = _PortfolioRepository.GetPortfolioHoldings(p.Portfolio_ID);

            if (portfolio == null)
            {
                return HttpNotFound();
            }
            
            return View(portfolio);
        }

        public ActionResult PortfolioKPIS(int portfolioID)
        {
            PortfolioKPIS kpis = _PortfolioRepository.GetPortfolioKeyPerformanceIndicators(portfolioID);
            return PartialView("_PartialKPIS",kpis);
        }


        public ActionResult HoldingsDetail(int portfolioID, int securityID)
        {
            HoldingsDetail detail = _PortfolioRepository.GetHoldingsDetail(portfolioID,securityID);
            return PartialView("_PartialHoldings",detail);
        }

        public ActionResult MonthlyPerformance(int portfolioID)
        {
            IEnumerable<MonthlyPerformance> performance = _PortfolioRepository.GetMonthlyPerformance(portfolioID);

            return PartialView("_PartialMonthlyPerformance",performance);
        }

		public FileResult DownloadReport(int portfolioID)
		{
			var user = UserManager.FindById(User.Identity.GetUserId());

			//AUTHORIZE USER
			if (!_PortfolioRepository.HasPortfolioPermissions(user.Id, portfolioID))
			{
				return null;
			}

			WebClient client = new WebClient();
			client.UseDefaultCredentials = true;
			string reportURL = "http://desktop-9pkcvov:8085/ReportServer_SSRS/Pages/ReportViewer.aspx?%2fPortfolioPresentation%2fCustomerReport&rs:Command=Render&rs:Format=PDF&Portfolio_ID="+portfolioID;
			return File(client.DownloadData(reportURL), "application/pdf");
		}
	}
}