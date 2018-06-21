using System.Collections.Generic;

namespace Portfolio.Web.Models
{
    public interface IPortfolioRepository
    {
        IEnumerable<PortfolioViewModel> Portfolios { get; }
    }
}