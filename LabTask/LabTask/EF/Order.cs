//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LabTask.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.ProductOrders = new HashSet<ProductOrder>();
        }
    
        public int Id { get; set; }
        public string Customer_Name { get; set; }
    
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
