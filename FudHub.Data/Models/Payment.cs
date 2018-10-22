using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Data.Models
{
    public class Payment : BaseModel
    {
        public int ID { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public string MeasurementUnit { get; set; }
        public string SellerID { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string Location { get; set; }
        //public PaStatus Status { get; set; }
        public DateTime Datestamp { get; set; }
    }
}
