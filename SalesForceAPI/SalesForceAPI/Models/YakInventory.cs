using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesForceAPI.Models
{
    public class YakInventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Qty { get; set; }
        public int LocationId { get; set; }
    }
}