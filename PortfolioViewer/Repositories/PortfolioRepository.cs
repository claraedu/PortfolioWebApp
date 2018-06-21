using PortfolioViewer.Interfaces;
using PortfolioViewer.Models.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioViewer.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private CustomerPortfolioEntities cpe;

        public PortfolioRepository()
        {
            cpe = new CustomerPortfolioEntities();
        }


        public HoldingsDetail GetHoldingsDetail(int portfolioID, int securityID)
        {
            return cpe.GetHoldingsDetail(portfolioID, securityID).First();
        }

        public IEnumerable<MonthlyPerformance> GetMonthlyPerformance(int portfolioID)
        {
            return cpe.GetMonthlyPerformance(portfolioID);
        }

        public IEnumerable<PortfolioHoldings> GetPortfolioHoldings(int portfolioID)
        {
            return cpe.GetPortfolioHoldings(portfolioID);
        }

        public PortfolioInfo GetPortfolioInfo(int portfolioID)
        {
            return cpe.GetPortfolioInfo(portfolioID).First();
        }

        public PortfolioKPIS GetPortfolioKeyPerformanceIndicators(int portfolioID)
        {
            return cpe.GetPortfolioKPIS(portfolioID).First();
        }

        public IEnumerable<Portfolio> GetPortfolios(int customerID)
        {
            return cpe.GetPortfolios(customerID);
        }

		public bool HasPortfolioPermissions(string userID, int portfolioID)
		{
			return (bool)cpe.HasPortfolioPermission(userID, portfolioID).First();
		}
	}
}