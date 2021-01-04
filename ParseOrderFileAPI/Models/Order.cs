using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParseOrderFileAPI.Models
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public string OrderLineNumber { get; set; }
        public string ProductNumber { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProductGroup { get; set; }
        public string OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }

    }
}