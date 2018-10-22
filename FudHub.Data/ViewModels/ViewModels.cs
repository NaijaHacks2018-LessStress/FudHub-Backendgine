using FudHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Data.ViewModels
{
    public class OrderData
    {
        public OrderBatch BatchData { get; set; }
        public List<Order> Items { get; set; }
    }

    public class Cart
    {
        public string MobileNo { get; set; }
        public List<Order> Items { get; set; } = new List<Order>();
        public PaymentMode PaymentMode { get; set; }
        public string EmailAddress { get; set; }
        public string IPAddress { get; set; }
        public string BrowserData { get; set; }
    }
}
