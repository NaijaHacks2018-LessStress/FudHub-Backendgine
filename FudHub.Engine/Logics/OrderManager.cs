using FudHub.Data.Models;
using FudHub.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Engine.Logics
{
    public class OrderManager
    {
        public List<Order> Load(string keyword = null, string location = null, int pageIndex = 1, int pageSize = 20)
        {
            var obj = MockUtility<Order>.GetList();
            return obj ?? new List<Order>();
        }

        public Order Get(int id)
        {
            var obj = Load().FirstOrDefault(c => c.ID == id);
            return obj;
        }

        public bool Add(Order order)
        {
            var obj = Load();
            obj.Add(order);
            return MockUtility<Order>.Save(obj);
        }

        public bool Remove(Order order)
        {
            var obj = Load();
            obj.Remove(order);
            return MockUtility<Order>.Save(obj);
        }
    }
}
