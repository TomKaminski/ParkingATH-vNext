using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IUserService : IEntityService<UserBaseDto, int>, IDependencyService
    {
        Task<ServiceResult<UserBaseDto>> ChangeEmailAsync(string email, string newEmail, string password);

        Task<ServiceResult<UserBaseDto, string>> GetPasswordChangeTokenAsync(string email);
        Task<ServiceResult<UserBaseDto, string>> GetSelfDeleteTokenAsync(string email);
        Task<ServiceResult<UserBaseDto>> ResetPasswordAsync(string token, string newPassword);
        Task<ServiceResult<UserBaseDto>> ChangePasswordAsync(string email, string password, string newPassword);

        Task<ServiceResult<int>> GetChargesAsync(string email, string hash);
        Task<ServiceResult<int>> AddChargesAsync(string email, int charges);

        Task<ServiceResult<UserBaseDto, UserPreferencesDto>> LoginAsync(string email, string password);

        Task<ServiceResult<UserBaseDto>> CheckLoginAsync(string email, string hash);
        Task<ServiceResult<bool>> CheckHashAsync(string email, string hash);

        new ServiceResult<UserBaseDto> Create(UserBaseDto entity);
        ServiceResult<UserBaseDto> Create(UserBaseDto entity, string password);
        new Task<ServiceResult<UserBaseDto>> CreateAsync(UserBaseDto entity);
        Task<ServiceResult<UserBaseDto>> CreateAsync(UserBaseDto entity, string password);

        ServiceResult<UserBaseDto> GetByEmail(string email);
        ServiceResult<UserBaseDto, UserPreferencesDto> GetByEmailWithPreferences(string email);

        Task<ServiceResult<UserBaseDto>> GetByEmailAsync(string email);
        Task<ServiceResult<UserBaseDto, UserPreferencesDto>> GetByEmailWithPreferencesAsync(string email);

        Task<ServiceResult<bool>> SelfDeleteAsync(string email, string token);

        Task<ServiceResult<int?>> OpenGateAsync(string email, string token);

        Task<ServiceResult<bool>> AccountExistsAsync(string email);

        Task<ServiceResult<bool>> IsAdmin(string email);

        Task<ServiceResult<UserBaseDto, UserPreferencesDto>> EditStudentInitialsAsync(UserBaseDto entity);

        Task<ServiceResult<int>> TransferCharges(string senderEmail, string recieverEmail, int numberOfCharges, string password);

        Task<ServiceResult<IEnumerable<UserAdminDto>>> GetAllForAdminAsync();
        Task<ServiceResult<IEnumerable<UserAdminDto>>> GetAllForAdminAsync(Expression<Func<UserBaseDto, bool>> predicate);

        Task<ServiceResult<UserAdminDto>> GetAdminAsync(int id);
        Task<ServiceResult<UserAdminDto>> GetAdminAsync(Expression<Func<UserBaseDto, bool>> predicate);

        Task<ServiceResult<int>> GetAdminAccountIdAsync();
    }
}
