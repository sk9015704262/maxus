using Maxus.Domain.Entities.PartialEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Interfaces
{
    public interface IDashBoardRepository
    {
        Task<tbl_DashboardCount?> GetCount(int id);
    }
}
