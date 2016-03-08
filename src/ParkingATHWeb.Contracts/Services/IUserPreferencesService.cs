using System;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IUserPreferencesService : IEntityService<UserPreferencesDto, int>, IDependencyService
    {
        Task<ServiceResult<Guid>> SetUserAvatarAsync(byte[] sourceImage, int userId, string folderPath);
        Task<ServiceResult<string>> DeleteProfilePhotoAsync(int userId, string folderPath);

        Task<ServiceResult<UserPreferencesDto>> SaveChartPreferenceAsync(UserPreferenceChartSettingsDto userPreferenceChartDto);
    }
}
