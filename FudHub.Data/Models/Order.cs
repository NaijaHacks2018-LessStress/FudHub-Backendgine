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
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Qty { get; set; }
        public double Amount { get; set; }
        public DateTime Datestamp { get; set; }
    }

    public class OrderBatch : BaseModel
    {
        public int  OrderID { get; set; }
        public bool? BuyerIsRegistered { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Datestamp { get; set; }
        public OrderStatus Status { get; set; }
    }
}
