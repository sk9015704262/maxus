using Maxus.Application.DTOs.dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.Interfaces
{
    public interface IDashBoardService
    {
        Task<GetDashBoardResponse?> Getcount(int id);
    }
}
