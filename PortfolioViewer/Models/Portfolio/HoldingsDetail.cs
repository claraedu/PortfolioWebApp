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
    
    public partial class HoldingsDetail
    {
        public string Security_Name { get; set; }
        public int Security_ID { get; set; }
        public System.DateTime Date_From { get; set; }
        public System.DateTime Date_To { get; set; }
        public int Quantity { get; set; }
        public decimal MarketValue { get; set; }
        public string Type { get; set; }
    }
}
