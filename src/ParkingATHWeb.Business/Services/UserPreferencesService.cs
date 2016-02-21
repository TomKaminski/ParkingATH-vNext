using AutoMapper;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.Business.Services
{
    public class UserPreferenesService:EntityService<UserPreferencesDto,UserPreferences, int>, IUserPreferencesService
    {
        private readonly IUserPreferencesRepository _repository;

        public UserPreferenesService(IUserPreferencesRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
        }
    }
}
