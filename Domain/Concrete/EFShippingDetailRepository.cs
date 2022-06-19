using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFShippingDetailRepository : IShippingDetailRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<ShippingDetail> Orders
        {
            get { return context.ShippingDetails; }
        }
        public void ProcessOrder(Cart cart, ShippingDetail shippingDetails)
        {
            shippingDetails.PartAndAmount = cart.GetSumOfLines();
            context.ShippingDetails.Add(shippingDetails);
            context.SaveChanges();
        }
    }
}
