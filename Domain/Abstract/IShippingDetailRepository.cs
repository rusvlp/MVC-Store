using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IShippingDetailRepository
    {
        IEnumerable<ShippingDetail> Orders { get; }
        void ProcessOrder(Cart cart, ShippingDetail shippingDetails);
    }
}
