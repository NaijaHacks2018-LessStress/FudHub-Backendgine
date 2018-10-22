using FudHub.Data.Models;
using FudHub.Data.ViewModels;
using FudHub.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Engine.Logics
{
    public class OrderBatchManager
    {
        public List<Order> Load(string keyword = null, string location = null, int pageIndex = 1, int pageSize = 20)
        {
            var obj = MockUtility<Order>.GetList();
            return obj;
        }

        public Order Get(int id)
        {
            var obj = Load().FirstOrDefault(c => c.ID == id);
            return obj;
        }
        
    }
}
