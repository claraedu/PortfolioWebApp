//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortfolioViewer.Models.Portfolio
{
    using System;
    
    public partial class PortfolioKPIS
    {
        public int Portfolio_ID { get; set; }
        public Nullable<double> stdDeviationPortfolio { get; set; }
        public Nullable<double> stdDeviationBenchmark { get; set; }
        public Nullable<decimal> Mean_Delta { get; set; }
        public Nullable<decimal> ARPortfolio { get; set; }
        public Nullable<decimal> ARBenchmark { get; set; }
        public Nullable<decimal> TrackingError { get; set; }
        public Nullable<decimal> InformationRatio { get; set; }
    }
}
