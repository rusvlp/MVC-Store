using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> LineCollection = new List<CartLine>();

        public List<CartLine> GetCartLines()
        {
            List<CartLine> toRet = new List<CartLine>();
            foreach(CartLine c in LineCollection)
            {
                toRet.Add(c);
            }

            return toRet;
        }

        public void AddItem(Part part, int quantity)
        {
            CartLine line = LineCollection
                .Where(g => g.Part.Id == part.Id)
                .FirstOrDefault();

            if (line == null)
            {
                if (quantity > part.Quantity)
                {
                    throw new Exception();
                }
                    
                LineCollection.Add(new CartLine
                {
                    Part = part,
                    Quantity = quantity
                });
            }
            else
            {
                if (line.Quantity + quantity > part.Quantity)
                {
                    throw new Exception();
                }
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Part part)
        {
            LineCollection.RemoveAll(l => l.Part.Id == part.Id);
        }

        public decimal ComputeTotalValue()
        {
            return LineCollection.Sum(e => e.Part.Price * e.Quantity);

        }
        public void Clear()
        {
            LineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return LineCollection; }
        }

        public string GetSumOfLines()
        {
            string res = "";
            foreach (CartLine cl in LineCollection)
            {
                res += cl.Part.Name + ", amount: " + cl.Quantity + ", price: " + cl.Part.Price * cl.Quantity + ", ";
            }
            res += "total price: " + ComputeTotalValue();
            return res;
        }
    }

    public class CartLine
    {
        public Part Part { get; set; }
        public int Quantity { get; set; }
    }
}

