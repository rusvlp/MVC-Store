using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFPartRepository : IPartRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Part> Parts
        {
            get { return context.Parts; }
        }

        public Part DeletePart(int gameId)
        {
            Part dbEntry = context.Parts.Find(gameId);
            if (dbEntry != null)
            {
                context.Parts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SavePart(Part part)
        {
            if (part.Id == 0)
                context.Parts.Add(part);
            else
            {
                Part dbEntry = context.Parts.Find(part.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = part.Name;
                    dbEntry.Description = part.Description;
                    dbEntry.Price = part.Price;
                    dbEntry.Category = part.Category;
                    dbEntry.Quantity = part.Quantity;
                }
            }
            context.SaveChanges();
        }
    }
}
