﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CustomerPortfolioEntities : DbContext
    {
        public CustomerPortfolioEntities()
            : base("name=CustomerPortfolioEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<CustomerHoldings> GetCustomerHoldings(Nullable<int> customerID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CustomerHoldings>("GetCustomerHoldings", customerIDParameter);
        }
    
        public virtual ObjectResult<Customer> GetCustomers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Customer>("GetCustomers");
        }
    
        public virtual ObjectResult<MonthlyPerformance> GetMonthlyPerformance(Nullable<int> customerID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MonthlyPerformance>("GetMonthlyPerformance", customerIDParameter);
        }
    
        public virtual ObjectResult<PortfolioInfo> GetPortfolioInfo(Nullable<int> customerID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PortfolioInfo>("GetPortfolioInfo", customerIDParameter);
        }
    
        public virtual ObjectResult<PortfolioKPIS> GetPortfolioKPIS(Nullable<int> customerID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PortfolioKPIS>("GetPortfolioKPIS", customerIDParameter);
        }
    
        public virtual ObjectResult<HoldingsDetail> GetHoldingsDetail(Nullable<int> customerID, Nullable<int> securityID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            var securityIDParameter = securityID.HasValue ?
                new ObjectParameter("SecurityID", securityID) :
                new ObjectParameter("SecurityID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HoldingsDetail>("GetHoldingsDetail", customerIDParameter, securityIDParameter);
        }
    }
}