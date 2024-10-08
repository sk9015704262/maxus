using AutoMapper;
using Maxus.Application.DTOs.Client;
using Maxus.Application.DTOs.dashboard;
using Maxus.Application.Interfaces;
using Maxus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.Services
{
    public class dashBoardService : IDashBoardService
    {
        private readonly IDashBoardRepository dashBoardRepository;
        private readonly IMapper _mapper;

        public dashBoardService(IDashBoardRepository dashBoardRepository ,  IMapper mapper)
        {
            this.dashBoardRepository = dashBoardRepository;
            _mapper = mapper;
        }

        public async Task<GetDashBoardResponse?> Getcount(int id)
        {
            try
            {
                var Count = await dashBoardRepository.GetCount(id);
                return _mapper.Map<GetDashBoardResponse>(Count);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving Count by ID.", ex);
            }
        }
    }
}
