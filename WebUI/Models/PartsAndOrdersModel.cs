using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class PartsAndOrdersModel
    {
        public IEnumerable<Part> Parts { get; set; }
        public IEnumerable<ShippingDetail> Orders { get; set; }
    }
}