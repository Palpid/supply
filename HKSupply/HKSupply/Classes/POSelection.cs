using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Classes
{
    public class POSelection
    {
        public string PoNumber { get; set; }
        public string Supplier { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryStart { get; set; }
        public DateTime? DeleveryEnd { get; set; }
        public int OriginalQuantity { get; set; }
        public int TotalQuantity { get; set; }
        public int DeliveredQuantity { get; set; }
        public int CanceledQuantity { get; set; }
        public int PendingQuantity { get; set; }
        public string DocStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Fulfillment { get; set; }
    }
}
