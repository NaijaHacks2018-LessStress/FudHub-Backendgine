using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Data.Models
{
    public class Seller : BaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string EmailAddress { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public SellerStatus Status { get; set; }
        public DateTime Datestamp { get; set; }

    }
}
