using FudHub.Data.Models;
using FudHub.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Engine.Logics
{
    public class SellerManager
    {
        public List<Seller> Load(string keyword = null, string location = null, int pageIndex = 1, int pageSize = 20)
        {
            var obj = MockUtility<Seller>.GetList();

            if (!string.IsNullOrEmpty(keyword))
                obj = obj.Where(s => s.Username.Contains(keyword) || s.Name.Contains(keyword) || s.Name.ToLower() == keyword.ToLower()).ToList();

            return obj ?? new List<Seller>();
        }

        public Seller Get(string usernameOrEmail)
        {
            var obj = Load().FirstOrDefault(s => s.Username == usernameOrEmail || s.EmailAddress == usernameOrEmail);
            return obj;
        }

        public (bool status, string message) Add(Seller seller)
        {
            try
            {
                var obj = Load();

                if (string.IsNullOrEmpty(seller.Name))
                    return (false, $"Pls provide your name");

                if (string.IsNullOrEmpty(seller.MobileNo1) && string.IsNullOrEmpty(seller.MobileNo2))
                    return (false, $"Pls provide a valid contact number");

                if (obj.Any(s => s.Username == seller.Username))
                    return (false, "Username already exist! Please try with another");
                else if (obj.Any(s => s.EmailAddress == seller.EmailAddress))
                    return (false, "Email address has already been used");
                else if (obj.Any(s => s.MobileNo1 == seller.MobileNo1 || s.MobileNo1 == seller.MobileNo2 ||
                                       s.MobileNo2 == seller.MobileNo2 || s.MobileNo2 == seller.MobileNo2))
                    return (false, $"Mobile number {seller.MobileNo1 ?? seller.MobileNo2} has already been used");

                if (string.IsNullOrEmpty(seller.State) || string.IsNullOrEmpty(seller.LGA) || string.IsNullOrEmpty(seller.Location))
                    return (false, $"Pls enter your state, local govt. area and location");


                if (string.IsNullOrEmpty(seller.MobileNo1) && !string.IsNullOrEmpty(seller.MobileNo2))
                    seller.MobileNo1 = seller.MobileNo2;
                seller.ID = obj.Max(s => s.ID) + 1;
                seller.Status = Data.SellerStatus.Active;
                seller.Datestamp = AppUtility.UTC();

                obj.Add(seller);
                bool r = MockUtility<Seller>.Save(obj);
                return (r, r ? "Seller has been registed" : "Failed to save seller details at this time, pls try again!");
            }
            catch (Exception ex)
            {
                return (false, Helper.PrettyEx(ex));
            }
        }

        public (bool status, string message) Remove(Seller seller)
        {
            try
            {
                var obj = Load();
                obj.Remove(seller);
                var r = MockUtility<Seller>.Save(obj);
                return (r, r ? "Seller has been removed" : "Failed to remove seller at this time, pls try again!");
            }
            catch (Exception ex)
            {
                return (false, Helper.PrettyEx(ex));
            }
        }
    }
}
