using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IUserService : IEntityService<UserBaseDto,int>, IDependencyService
    {
        Task<ServiceResult<UserBaseDto>> ChangeEmailAsync(string email, string newEmail, string password);

        Task<ServiceResult<string>> GetPasswordChangeTokenAsync(string email);
        Task<ServiceResult<UserBaseDto>> ResetPasswordAsync(string token, string newPassword);
        Task<ServiceResult<UserBaseDto>> ChangePasswordAsync(string email, string password, string newPassword);

        Task<ServiceResult<int>> GetChargesAsync(string email);
        Task<ServiceResult<int>> AddChargesAsync(string email, int charges);

        Task<ServiceResult<UserBaseDto>> LoginMvcAsync(string email, string password);

        Task<ServiceResult<UserBaseDto>> LoginFirstTimeAsync(string email, string password);
        Task<ServiceResult<UserBaseDto>> CheckLogin(string email, string token);
        Task<ServiceResult<bool>> CheckHash(string email, string token);

        new ServiceResult<UserBaseDto> Create(UserBaseDto entity);
        ServiceResult<UserBaseDto> Create(UserBaseDto entity, string password);
        new Task<ServiceResult<UserBaseDto>> CreateAsync(UserBaseDto entity);
        Task<ServiceResult<UserBaseDto>> CreateAsync(UserBaseDto entity, string password);

        ServiceResult<UserBaseDto> GetByEmail(string email);

        Task<ServiceResult<UserBaseDto>> GetByEmailAsync(string email);

        Task<ServiceResult<bool>> SelfDelete(string email, string password);

        Task<ServiceResult<int?>> OpenGateAsync(string email, string token);

        Task<ServiceResult<bool>> AccountExistsAsync(string email);

        Task<ServiceResult<bool>> IsAdmin(string email);

        Task<ServiceResult<UserBaseDto>> EditStudentInitialsAsync(UserBaseDto entity);
    }
}
