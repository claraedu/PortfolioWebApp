using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace Portfolio.Web.Models
{
    public class AppDbRepository : IPortfolioRepository
    {
        public IEnumerable<PortfolioViewModel> Portfolios
        {
            get
            {
                var output = new List<PortfolioViewModel>();
                using (AppEntitties _context = new AppEntitties())
                {
                    var pIds = new List<int>();
                    switch (HttpContext.Current.User.IsInRole("admin"))
                    {
                        case true:
                            pIds = _context.portfolio.Select(p => p.portfolio_id).ToList();
                            break;
                        case false:
                        default:
                        {
                            var id = HttpContext.Current.User.Identity.GetUserId();
                            pIds = _context.portfolio
                                .Where(p => p.tblUser.fldGuid == id)
                                .Select(p => p.portfolio_id).ToList();
                        }
                            break;
                    }

                    var data =
                    (
                        from pos in _context.position
                        join p2b in _context.portfolio_to_benchmark on pos.portfolio_id equals p2b.portfolio_id
                        join b in _context.benchmark_index on p2b.benchmark_id equals b.benchmark_id
                        select new
                        {
                            portId = pos.portfolio.portfolio_id,
                            portName = pos.portfolio.name,
                            portInstName = pos.instrument.description,
                            portInstVol = pos.volume,
                            posBenchIndexName = b.index.name,
                            posBenchIndexWeight = b.weight
                        }
                    ).Where(p => pIds.Contains(p.portId)).Distinct().ToList();

                    foreach (var entry in data)
                    {
                        if (!output.Exists(p => p.Id == entry.portId))
                        {
                            var p = new PortfolioViewModel();
                            p.Id = entry.portId;
                            p.Name = entry.portName;
                            p.Asset.Add(entry.portInstName);
                            p.AssetVolume.Add(entry.portInstVol);
                            p.BenchIndex.Add(entry.posBenchIndexName);
                            p.IndexWeight.Add(entry.posBenchIndexWeight);
                            p.PMetrics = _context.spSELPortfolioMetrics(p.Id).Where(g => g.fldType == "Portfolio")
                                    .Select(g => g.fldMetric).ToList();
                            p.PMetricValues = _context.spSELPortfolioMetrics(p.Id).Where(g =>g.fldType == "Portfolio")
                                    .Select(g => g.fldValue).ToList();
                            p.BMetrics = _context.spSELPortfolioMetrics(p.Id).Where(g => g.fldType == "Benchmark")
                                .Select(g => g.fldMetric).ToList();
                            p.BMetricValues = _context.spSELPortfolioMetrics(p.Id).Where(g => g.fldType == "Benchmark")
                               .Select(g => g.fldValue).ToList();
                            p.TimePeriod = _context.spSELPortfolioReport(p.Id)
                                            .Where(g => g.fldPerformance == "Portfolio")
                                            .Select(g => g.fldMonth)
                                            .ToList();
                            p.PortfolioPerformance = _context.spSELPortfolioReport(p.Id)
                                                    .Where(g => g.fldPerformance == "Portfolio")
                                                    .Select(g => g.fldValue)
                                                    .ToList();
                            p.BenchmarkPerformance = _context.spSELPortfolioReport(p.Id)
                                                    .Where(g => g.fldPerformance == "Benchmark")
                                                    .Select(g => g.fldValue)
                                                    .ToList();
                            output.Add(p);
                        }
                        else
                        {
                            var port = output.FindIndex(p => p.Id == entry.portId);
                            if (output[port].Asset.IndexOf(entry.portInstName) < 0)
                            {
                                output[port].Asset.Add(entry.portInstName);
                                output[port].AssetVolume.Add(entry.portInstVol);
                            }
                            if (output[port].BenchIndex.IndexOf(entry.posBenchIndexName) < 0)
                            {
                                output[port].BenchIndex.Add(entry.posBenchIndexName);
                                output[port].IndexWeight.Add(entry.posBenchIndexWeight);
                            }
                            
                        }
                    }

                    
                }
                return output;
            }
        }
    }
}