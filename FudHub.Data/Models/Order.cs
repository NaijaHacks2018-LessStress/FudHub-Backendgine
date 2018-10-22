using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Data.Models
{
    public class Order : BaseModel
    {
        public int ID { get; set; }
        public string ProductID { get; set; }
        public int Qty { get; set; }
        public double Amount { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string BuyerID { get; set; }
        public bool? BuyerIsRegistered { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Datestamp { get; set; }
    }
}
