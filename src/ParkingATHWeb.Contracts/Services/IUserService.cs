using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.UserProfile;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IUserService:IEntityService<UserDto>, IDependencyService
    {
        Task<ServiceResult<string>> ChangeEmailAsync(string email, string newEmail, string token);

        Task<ServiceResult<string>> GetCodeTokenAsync(string email);
        Task<ServiceResult<string>> GetCodeTokenAsync(string email, string hash);
        Task<ServiceResult<string>> ChangeCodeAsync(string email, string token);
        Task<ServiceResult<bool?>> ChangeCodeAsync(string email, string password, string newPassword);

        Task<ServiceResult<int>> GetChargesAsync(string email);
        Task<ServiceResult<int>> AddChargesAsync(string email, int charges);

        Task<ServiceResult<bool>> LoginFirstTimeMvcAsync(string email, string password);

        Task<ServiceResult<string>> LoginFirstTimeAsync(string email, string password);
        Task<ServiceResult<string>> CheckLogin(string email, string hash);
        Task<ServiceResult<bool>> CheckHash(string email, string hash);

        new ServiceResult<string> Create(UserDto entity);
        ServiceResult<string> Create(UserDto entity, string password);
        new Task<ServiceResult<string>> CreateAsync(UserDto entity);
        Task<ServiceResult<string>> CreateAsync(UserDto entity, string password);

        ServiceResult<UserDto> GetByEmail(string email);

        Task<ServiceResult<UserDto>> GetByEmailAsync(string email);

        Task<ServiceResult<bool>> SelfDelete(string email, string password);

        Task<ServiceResult<int?>> OpenGateAsync(string email, string hash);

        Task<ServiceResult<bool>> AccountExistsAsync(string email);

        Task<ServiceResult<bool>> IsAdmin(string email);

        Task<ServiceResult> EditStudentInitialsAsync(UserDto entity);
    }
}
