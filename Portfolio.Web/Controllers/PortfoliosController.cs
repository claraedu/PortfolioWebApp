using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        //public async system.threading.tasks.task<actionresult> showreportasync(int id)
        //{
        //    var values = new dictionary<string, string>
        //    {
        //        { "rs:command", "render" },
        //        { "rc:linktarget", "main" },
        //        { "rs:format", "html5.0" },
        //        { "rc:parameters", "false" },
        //        { "portfolio_id", id.tostring() },
        //    };

        //    var content = new formurlencodedcontent(values);
        //    httpclient client = new httpclient();
        //    string server = "http://desktop-7lvuu3i/reportserver_ssrs/pages/reportviewer.aspx?";
        //    string path = "%2freport+project4%2freport1";
        //    string url = server + path;
        //    var response = await client.postasync(url, content);
        //    var responsestring = await response.content.readasstringasync();
        //    return new redirectresult(responsestring);
        //}
    }
}
