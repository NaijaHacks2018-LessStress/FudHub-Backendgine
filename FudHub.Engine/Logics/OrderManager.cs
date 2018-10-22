using FudHub.Data;
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
    public class OrderManager
    {
        public List<Order> Load(string keyword = null, string location = null, int pageIndex = 1, int pageSize = 20)
        {
            var obj = MockUtility<Order>.GetList();
            return obj;
        }

        public List<OrderBatch> LoadBatch(string keyword = null, string location = null, int pageIndex = 1, int pageSize = 20)
        {
            var obj = MockUtility<OrderBatch>.GetList();
            return obj;
        }

        public (OrderData cart, string message) Get(int id)
        {
            try
            {
                var obj = LoadBatch().FirstOrDefault(c => c.OrderID == id);
                if (obj == null) return (null, "Order not found");

                var items = Load().Where(o => o.OrderID == id);
                if (items == null || items.Count() == 0)
                    return (null, "Order items not found");

                var orderData = new OrderData
                {
                    BatchData = obj,
                    Items = items.ToList()
                };

                return (orderData, "Order details has been loaded");
            }
            catch (Exception ex)
            {
                return (null, Helper.PrettyEx(ex));
            }
        }

        public bool Cancel(int orderID)
        {
            var obj = LoadBatch();
            obj.FirstOrDefault(c => c.OrderID == orderID).Status = OrderStatus.Cancelled;
            bool r = MockUtility<OrderBatch>.Save(obj);
            return r;
        }

        public (bool status, string message) Add(Cart order)
        {
            try
            {
                if (order == null || order.Items.Count == 0)
                    return (false, "Cart is empty, pls add items and try again!");

                if (string.IsNullOrEmpty(order.MobileNo))
                    return (false, "Mobile number has not been supplied");

                var products = new ProductManager().Load();

                foreach (var i in order.Items)
                {
                    if (i.ProductID == 0) return (false, $"Invalid product ID found in cart");
                    //if (i.Amount == 0) return (false, $"Item {i.ProductID} does not have amount");
                    if (i.Qty == 0) return (false, $"Item {i.ProductID} quantity cannot be zero");

                    if (!products.Any(p => p.ID == i.ProductID))
                        return (false, $"Invalid product ID found in cart");

                    i.Amount = products.FirstOrDefault(p => p.ID == i.ProductID).Amount;
                }

                var obj_order = Load();
                var order_items = new List<Order>();
                int count = obj_order.Count > 0 ? obj_order.Max(p => p.ID) + 1 : 1;
                var obj_orderBatch = LoadBatch();

                foreach (var i in order.Items)
                {
                    i.ID = count++;
                    i.OrderID = obj_orderBatch.Count() + 1;
                    i.Datestamp = AppUtility.UTC();
                    order_items.Add(i);
                }

                var batch = new OrderBatch
                {
                    OrderID = obj_orderBatch.Count > 0 ? obj_orderBatch.Max(p => p.OrderID) + 1 : 1,
                    MobileNo = order.MobileNo,
                    EmailAddress = order.EmailAddress,
                    BuyerIsRegistered = false,
                    PaymentMode = order.PaymentMode,
                    Datestamp = AppUtility.UTC(),
                    Status = Data.OrderStatus.Pending
                };

                obj_order.AddRange(order.Items);
                obj_orderBatch.Add(batch);

                bool r1 = MockUtility<Order>.Save(obj_order);
                if (!r1) return (false, "Failed to save order details at this time, pls check details and try again!");
                bool r2 = MockUtility<OrderBatch>.Save(obj_orderBatch);
                if (!r2) return (false, "Failed to complete order at this time, pls check details and try again!");

                return (true, "Order placement complete");
            }
            catch (Exception ex)
            {
                return (false, Helper.PrettyEx(ex));
            }
        }

        public bool Remove(Order order)
        {
            var obj = Load();
            obj.Remove(order);
            return MockUtility<Order>.Save(obj);
        }
    }
}
