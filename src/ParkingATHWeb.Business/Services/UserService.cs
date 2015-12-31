using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Business.Services
{
    public class UserService : EntityService<UserBaseDto, User, int>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IGateUsageRepository _gateUsageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository repository, IUnitOfWork unitOfWork, IGateUsageRepository gateUsageRepository, IPasswordHasher passwordHasher)
            : base(repository, unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _gateUsageRepository = gateUsageRepository;
            _passwordHasher = passwordHasher;
        }

        public new ServiceResult<string> Create(UserBaseDto entity)
        {
            var code = GetUniqueKey();
            var saltHash = _passwordHasher.CreateHash(code);
            entity.PasswordSalt = saltHash.Salt;
            entity.PasswordHash = saltHash.Hash;
            _repository.Add(Mapper.Map<User>(entity));

            _unitOfWork.Commit();
            return ServiceResult<string>.Success(code);
        }

        public ServiceResult<UserBaseDto> Create(UserBaseDto entity, string password)
        {
            var saltHash = _passwordHasher.CreateHash(password);
            entity.PasswordSalt = saltHash.Salt;
            entity.PasswordHash = saltHash.Hash;
            var user = _repository.Add(Mapper.Map<User>(entity));

            _unitOfWork.Commit();
            return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(user));
        }

        public new async Task<ServiceResult<UserBaseDto>> CreateAsync(UserBaseDto entity)
        {
            var code = GetUniqueKey();
            var saltHash = _passwordHasher.CreateHash(code);
            entity.PasswordSalt = saltHash.Salt;
            entity.PasswordHash = saltHash.Hash;
            var user = _repository.Add(Mapper.Map<User>(entity));
            await _unitOfWork.CommitAsync();
            return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(user));
        }

        public async Task<ServiceResult<UserBaseDto>> CreateAsync(UserBaseDto entity, string password)
        {
            var saltHash = _passwordHasher.CreateHash(password);
            entity.PasswordSalt = saltHash.Salt;
            entity.PasswordHash = saltHash.Hash;
            var userDto = _repository.Add(Mapper.Map<User>(entity));
            await _unitOfWork.CommitAsync();
            return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(userDto));
        }

        public ServiceResult<UserBaseDto> GetByEmail(string email)
        {
            var stud = _repository.FirstOrDefault(x => x.Email == email);
            return ServiceResult<UserBaseDto>.Success(stud == null ? null : Mapper.Map<UserBaseDto>(stud));
        }

        public async Task<ServiceResult<string>> ChangeEmailAsync(string email, string newEmail, string code)
        {
            if (await _repository.FirstOrDefaultAsync(x => x.Email == newEmail) == null)
            {
                var entity = await _repository.FirstOrDefaultAsync(x => x.Email == email);
                if (_passwordHasher.ValidatePassword(code, entity.PasswordHash, entity.PasswordSalt))
                {
                    entity.Email = newEmail;
                    _repository.Edit(entity);
                    await _unitOfWork.CommitAsync();
                    return ServiceResult<string>.Success(code);
                }
                return ServiceResult<string>.Failure("Niepoprawny login lub hasło");
            }
            return ServiceResult<string>.Failure("Adres email jest już zajęty");
        }

        //TODO: Use tokens
        public async Task<ServiceResult<string>> GetPasswordChangeTokenAsync(string email, string hash)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResult<string>> GetPasswordChangeTokenAsync(string email)
        {
            throw new NotImplementedException();

        }

        public async Task<ServiceResult<string>> ChangePasswordAsync(string email, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool?>> ChangePasswordAsync(string email, string password, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<int>> GetChargesAsync(string email)
        {
            var stud = await _repository.FirstOrDefaultAsync(x => x.Email == email);
            return stud != null
                ? ServiceResult<int>.Success(stud.Charges)
                : ServiceResult<int>.Failure("Użytkownik nie został znaleziony");
        }

        public async Task<ServiceResult<int>> AddChargesAsync(string email, int charges)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Email == email);
            entity.Charges += charges;
            _repository.Edit(entity);
            await _unitOfWork.CommitAsync();
            return ServiceResult<int>.Success(entity.Id);
        }

        public async Task<ServiceResult<UserBaseDto>> LoginFirstTimeMvcAsync(string email, string password)
        {
            var stud = await _repository.FirstOrDefaultAsync(x => x.Email == email);
            if (stud != null && _passwordHasher.ValidatePassword(password, stud.PasswordHash, stud.PasswordSalt) && !stud.LockedOut)
            {
                return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(stud));
            }
            return ServiceResult<UserBaseDto>.Failure("Niepoprawny login lub hasło");
        }

        //TODO: Token based authentication
        public async Task<ServiceResult<string>> LoginFirstTimeAsync(string email, string password)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResult<string>> CheckLogin(string email, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> CheckHash(string email, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> SelfDelete(string email, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<int?>> OpenGateAsync(string email, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<UserBaseDto>> GetByEmailAsync(string email)
        {
            var stud = await _repository.FirstOrDefaultAsync(x => x.Email == email);
            return stud == null
                ? ServiceResult<UserBaseDto>.Failure("Użytkownik nie istnieje")
                : ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(stud));
        }

        public async Task<ServiceResult<bool>> AccountExistsAsync(string email)
        {
            return ServiceResult<bool>.Success(await _repository.FirstOrDefaultAsync(x => x.Email == email) != null);
        }

        public async Task<ServiceResult<bool>> IsAdmin(string email)
        {
            return ServiceResult<bool>.Success((await _repository.FirstOrDefaultAsync(x => x.Email == email)).IsAdmin);
        }

        public async Task<ServiceResult> EditStudentInitialsAsync(UserBaseDto entity)
        {
            var student = await _repository.FirstOrDefaultAsync(x => x.Email == entity.Email);
            student.Name = entity.Name;
            student.LastName = entity.LastName;
            _repository.Edit(student);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        #region Helpers

        private static string GetUniqueKey()
        {
            const int size = 8;
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }
        #endregion
    }
}
