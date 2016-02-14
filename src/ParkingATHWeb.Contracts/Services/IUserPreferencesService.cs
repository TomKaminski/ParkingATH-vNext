using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IUserPreferencesService : IEntityService<UserPreferencesDto, int>, IDependencyService
    {
    }
}
