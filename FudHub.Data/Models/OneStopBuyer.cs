using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Data.Models
{
    public class OneStopBuyer : BaseModel
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
        public string Datestamp { get; set; }
    }
}
