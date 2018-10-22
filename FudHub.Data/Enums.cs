using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Data
{
    public enum PaymentMode
    {
        OnlinePay = 1,
        BankTransfer = 2,
        Cash = 3,
        PayOnDelivery = 4
    }

    public enum OrderStatus
    {
        Complete = 1,
        Pending = 2,
        Removed = 3
    }

    public enum SellerStatus
    {
        Active = 1,
        Pending = 2,
        Blocked = 3,
        Deactivated = 4
    }

    public enum ProductStatus
    {
        Active = 1,
        SoldOut = 2,
        Removed = 3
    }
}
