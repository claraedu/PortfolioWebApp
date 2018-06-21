using PortfolioViewer.Models.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioViewer.Interfaces
{
    public interface IPortfolioRepository
    {
        IEnumerable<Portfolio> GetPortfolios(int customerID);
        IEnumerable<PortfolioHoldings> GetPortfolioHoldings(int portfolioID);
        IEnumerable<MonthlyPerformance> GetMonthlyPerformance(int portfolioID);

        PortfolioInfo GetPortfolioInfo(int portfolioID);
        PortfolioKPIS GetPortfolioKeyPerformanceIndicators(int portfolioID);
        HoldingsDetail GetHoldingsDetail(int portfolioID, int securityID);

		Boolean HasPortfolioPermissions(string userID, int portfolioID);
    }
}
