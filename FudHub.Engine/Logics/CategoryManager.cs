using FudHub.Data.Models;
using FudHub.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Engine.Logics
{
    public class CategoryManager
    {
        public List<Category> Load()
        {
            var obj = MockUtility<Category>.GetList();
            return obj;
        }

        public Category Get(int id)
        {
            var obj = Load().FirstOrDefault(c => c.ID == id);
            return obj;
        }

        public (bool status, string message) Add(Category category)
        {
            try
            {
                var obj = Load();

                if (obj.Any(c => c.Name.ToLower() == category.Name.ToLower()))
                    return (false, "Category already exist");

                category.ID = obj.Count > 0 ? obj.Max(p => p.ID) + 1 : 1;
                obj.Add(category);
                var r = MockUtility<Category>.Save(obj);
                return (r, r ? "New Category has been added" : "Failed to add category at this time, pls try again!");
            }
            catch (Exception ex)
            {
                return (false, Helper.PrettyEx(ex));
            }
        }

        public bool Remove(Category category)
        {
            var obj = Load();
            obj.Remove(category);
            return MockUtility<Category>.Save(obj);
        }
    }
}
