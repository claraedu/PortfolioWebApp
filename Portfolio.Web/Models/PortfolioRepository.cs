using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Portfolio.Entities;

namespace Portfolio.Web.Models
{
    public class PortfolioRepository : IPortfolioRepository
    {
        //protected readonly DbContext Context;

        /*public PortfolioRepository(DbContext context)
        {
            Context = context;
        }*/

        private List<Entities.Portfolio> portfolios = new List<Entities.Portfolio>();
        private int _nextId = 1;

        public PortfolioRepository()
        {
            // Add products for the Demonstration
            
            Dictionary<String, int> assetsA = new Dictionary<string, int>()
            {
                {"SSAB B", 40000}, {"AZN B", 20000}
            };

            Dictionary<String, int> assetsB = new Dictionary<string, int>()
            {
                {"SSAB B", 20000}, {"AZN B", 10000}, {"S001046", 5000000}
            };

            Dictionary<String, double> benchA = new Dictionary<string, double>()
            {
                {"OMXS30", 1.0}
            };

            Dictionary<String, double> benchB = new Dictionary<string, double>()
            {
                {"OMXS30", 0.5},  {"OMXRBOND", 0.5}
            };

            Dictionary<String, double> metricA = new Dictionary<string, double>()
            {
                {"PR", 0.37518248},
                {"VOLAT", 0.474568236146896},
                {"TE", 0.351894699125692},
                {"IR", 3.53605430562533}
            };

            Dictionary<String, double> metricB = new Dictionary<string, double>()
            {
                {"PR", 0.17896104},
                {"VOLAT", 0.228297547556557},
                {"TE", 0.171836093409985},
                {"IR", 2.88630245883746}
            };

            Add(new Entities.Portfolio
            {
                Name = "Portfolio A", Assets = assetsA, Benchmark = benchA, Metrics = metricA
            });
            Add(new Entities.Portfolio
            {
                Name = "Portfolio B", Assets = assetsB, Benchmark = benchB, Metrics = metricB
            });
        }

        public IEnumerable<Entities.Portfolio> GetAll()
        {
            // TO DO : Code to get the list of all the records in database 
            return portfolios;
        }

        public Entities.Portfolio Get(int id)
        {
            // TO DO : Code to find a record in database 
            return portfolios.Find(p => p.Id == id);
        }

        public Entities.Portfolio Add(Entities.Portfolio item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to save record into database 
            item.Id = _nextId++;
            portfolios.Add(item);
            return item;
        }

        public bool Update(Entities.Portfolio item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database 
            int index = portfolios.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            portfolios.RemoveAt(index);
            portfolios.Add(item);
            return true;
        }

        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database 
            portfolios.RemoveAll(p => p.Id == id);
            return true;
        }
    }
}