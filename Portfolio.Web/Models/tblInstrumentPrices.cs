//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Portfolio.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblInstrumentPrices
    {
        public int fldInsrumentId { get; set; }
        public System.DateTime fldDate { get; set; }
        public decimal fldPrice { get; set; }
        public Nullable<System.DateTime> fldInsertDate { get; set; }
        public string fldUser { get; set; }
    
        public virtual instrument instrument { get; set; }
    }
}