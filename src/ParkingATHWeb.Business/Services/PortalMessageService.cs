using AutoMapper;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;

namespace ParkingATHWeb.Business.Services
{
    public class ChartService : IChartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
