using FudHub.Data.Models;
using FudHub.Engine.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Engine.Logics
{
    public class ProductManager
    {
        public List<Product> Load(string keyword = null, string location = null, int pageIndex = 1, int pageSize = 20)
        {
            var obj = MockUtility<Product>.GetList();

            if (!string.IsNullOrEmpty(keyword))
                obj = obj.Where(p => p.Title.Contains(keyword) || p.Title.ToLower() == keyword.ToLower() || p.Description.Contains(keyword)).ToList();

            return obj;
        }

        public Product Get(int ID)
        {
            var obj = Load().FirstOrDefault(p => p.ID == ID);
            return obj;
        }

        public (bool status, string message) Add(Product product)
        {
            try
            {
                var obj = Load();

                if (string.IsNullOrEmpty(product.Title))
                    return (false, "Product title has not been supplied");
                else if (string.IsNullOrEmpty(product.Description))
                    return (false, "Product description has not been supplied");
                else if (product.Category == null)
                    return (false, "Product category not set");
                else if (product.Category.ID == 0)
                    return (false, "Product category not set");
                else if (product.Category.ID > 0)
                {
                    // verify category
                    var cat = new CategoryManager().Load();
                    if (!cat.Any(c => c.ID == product.Category.ID))
                        return (false, "Invalid product category selected");
                }
                else if (product.Qty == 0)
                    return (false, "Quantity must be above zero");
                else if (product.Amount == 0)
                    return (false, "You've not specified product amount");

                product.ID = obj.Count > 0 ? obj.Max(p => p.ID) + 1 : 1;
                product.Status = Data.ProductStatus.Active;
                product.Datestamp = AppUtility.UTC();

                obj.Add(product);
                bool r = MockUtility<Product>.Save(obj);
                return (r, r ? "Product has been added" : "Failed to add product");
            }
            catch (Exception ex)
            {
                return (false, Helper.PrettyEx(ex));
            }
        }

        public (bool status, string message) Remove(Product product)
        {
            try
            {
                var obj = Load();
                obj.Remove(product);
                var r = MockUtility<Product>.Save(obj);
                return (r, r ? "Product has been removed" : "Failed to remove product at this time, pls try again!");
            }
            catch (Exception ex)
            {
                return (false, Helper.PrettyEx(ex));
            }
        }
    }
}
