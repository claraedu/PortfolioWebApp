using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers
{
    [Authorize]
    public class PortfoliosController : Controller
    {
        private readonly IPortfolioRepository _repository;

        public PortfoliosController(IPortfolioRepository repository)
        {
            _repository = repository;
        }

        // GET: Portfolios
        [Authorize]
        public ActionResult Index()
        {
            var userPortfolios = _repository.Portfolios
                .Select(x => new SimplePortfolio() {Id = x.Id, Name = x.Name}).ToList();
            ViewData.Add("portfolios", userPortfolios);
            return View();
        }

        public ActionResult PartialPortfolio(int id)
        {
            var renderPortfolio = _repository.Portfolios.First(p => p.Id == id);
            return PartialView(renderPortfolio);
        }

        public ActionResult ShowReport(int id)
        {
            string server = "http://desktop-7lvuu3i/ReportServer_SSRS/Pages/ReportViewer.aspx?";
            string path = "%2fReport+Project4%2fReport1";
            string url = server + path;

            url += "&rs:Command=Render";
            url += "&rc:LinkTarget=main";
            url += "&rs:Format=HTML5.0";
            url += "&rc:Parameters=false";
            url += $"&portfolio_ID={id}";
            return new RedirectResult(url);
        }
    }
}
