using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IPartRepository
    {
        IEnumerable<Part> Parts { get; }
        void SavePart(Part game);
        Part DeletePart(int gameId);
    }
}
