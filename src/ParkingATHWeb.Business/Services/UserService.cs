using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.Entity;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Business.Services
{
    public class UserService : EntityService<UserBaseDto, User, int>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IGateUsageRepository _gateUsageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;

        public UserService(IUserRepository repository, IUnitOfWork unitOfWork, IGateUsageRepository gateUsageRepository, IPasswordHasher passwordHasher, ITokenService tokenService, ITokenRepository tokenRepository)
            : base(repository, unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _gateUsageRepository = gateUsageRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
        }

        public new ServiceResult<UserBaseDto> Create(UserBaseDto entity)
        {
            var possibleUserExistResult = _repository.FirstOrDefault(x => x.Email == entity.Email);
            if (possibleUserExistResult == null || possibleUserExistResult.IsDeleted)
            {
                var code = GetUniqueKey();
                var saltHash = _passwordHasher.CreateHash(code);

                if (possibleUserExistResult == null)
                {
                    entity.PasswordSalt = saltHash.Salt;
                    entity.PasswordHash = saltHash.Hash;
                    var user = _repository.Add(Mapper.Map<User>(entity));
                    _unitOfWork.Commit();
                    return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(user));
                }
                possibleUserExistResult.PasswordHash = saltHash.Hash;
                possibleUserExistResult.PasswordSalt = saltHash.Salt;
                possibleUserExistResult.IsDeleted = false;
                _repository.Edit(Mapper.Map<User>(possibleUserExistResult));
                _unitOfWork.Commit();
                return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(possibleUserExistResult));
            }
            return ServiceResult<UserBaseDto>.Failure("Adres email jest już zajęty");
        }

        public ServiceResult<UserBaseDto> Create(UserBaseDto entity, string password)
        {
            var possibleUserExistResult = _repository.FirstOrDefault(x => x.Email == entity.Email);
            if (possibleUserExistResult == null || possibleUserExistResult.IsDeleted)
            {
                var saltHash = _passwordHasher.CreateHash(password);

                if (possibleUserExistResult == null)
                {
                    entity.PasswordSalt = saltHash.Salt;
                    entity.PasswordHash = saltHash.Hash;
                    var user = _repository.Add(Mapper.Map<User>(entity));
                    _unitOfWork.Commit();
                    return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(user));
                }
                possibleUserExistResult.PasswordHash = saltHash.Hash;
                possibleUserExistResult.PasswordSalt = saltHash.Salt;
                possibleUserExistResult.IsDeleted = false;
                _repository.Edit(Mapper.Map<User>(possibleUserExistResult));
                _unitOfWork.Commit();
                return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(possibleUserExistResult));
            }
            return ServiceResult<UserBaseDto>.Failure("Adres email jest już zajęty");
        }

        public new async Task<ServiceResult<UserBaseDto>> CreateAsync(UserBaseDto entity)
        {
            var possibleUserExistResult = await _repository.FirstOrDefaultAsync(x => x.Email == entity.Email);
            if (possibleUserExistResult == null || possibleUserExistResult.IsDeleted)
            {
                var code = GetUniqueKey();
                var saltHash = _passwordHasher.CreateHash(code);

                if (possibleUserExistResult == null)
                {
                    entity.PasswordSalt = saltHash.Salt;
                    entity.PasswordHash = saltHash.Hash;
                    var user = _repository.Add(Mapper.Map<User>(entity));
                    await _unitOfWork.CommitAsync();
                    return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(user));
                }
                possibleUserExistResult.PasswordHash = saltHash.Hash;
                possibleUserExistResult.PasswordSalt = saltHash.Salt;
                possibleUserExistResult.IsDeleted = false;
                _repository.Edit(Mapper.Map<User>(possibleUserExistResult));
                await _unitOfWork.CommitAsync();
                return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(possibleUserExistResult));
            }
            return ServiceResult<UserBaseDto>.Failure("Adres email jest już zajęty");
        }

        public async Task<ServiceResult<UserBaseDto>> CreateAsync(UserBaseDto entity, string password)
        {
            var possibleUserExistResult = await _repository.FirstOrDefaultAsync(x => x.Email == entity.Email);
            if (possibleUserExistResult == null || possibleUserExistResult.IsDeleted)
            {
                var saltHash = _passwordHasher.CreateHash(password);

                if (possibleUserExistResult == null)
                {
                    entity.PasswordSalt = saltHash.Salt;
                    entity.PasswordHash = saltHash.Hash;
                    var user = _repository.Add(Mapper.Map<User>(entity));
                    await _unitOfWork.CommitAsync();
                    return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(user));
                }
                possibleUserExistResult.PasswordHash = saltHash.Hash;
                possibleUserExistResult.PasswordSalt = saltHash.Salt;
                possibleUserExistResult.IsDeleted = false;
                _repository.Edit(Mapper.Map<User>(possibleUserExistResult));
                await _unitOfWork.CommitAsync();
                return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(possibleUserExistResult));
            }
            return ServiceResult<UserBaseDto>.Failure("Adres email jest już zajęty");
        }

        public ServiceResult<UserBaseDto> GetByEmail(string email)
        {
            var stud = _repository.FirstOrDefault(x => x.Email == email);
            return stud == null ? ServiceResult<UserBaseDto>.Failure("Użytkownik nie istnieje")
                : ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(stud));
        }

        public async Task<ServiceResult<UserBaseDto>> ChangeEmailAsync(string email, string newEmail, string password)
        {
            if ((await _repository.FirstOrDefaultAsync(x => x.Email == newEmail)) == null)
            {
                var entity = await _repository.FirstOrDefaultAsync(x => x.Email == email);
                if (entity != null && _passwordHasher.ValidatePassword(password, entity.PasswordHash, entity.PasswordSalt))
                {
                    entity.Email = newEmail;
                    _repository.Edit(entity);
                    await _unitOfWork.CommitAsync();
                    return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(entity));
                }
                return ServiceResult<UserBaseDto>.Failure("Niepoprawny login lub hasło");
            }
            return ServiceResult<UserBaseDto>.Failure("Adres email jest już zajęty");
        }

        public async Task<ServiceResult<UserBaseDto, string>> GetPasswordChangeTokenAsync(string email)
        {
            var entity = await _repository.Include(x => x.PasswordChangeToken).FirstOrDefaultAsync(x => x.Email == email);
            var resetPasswordToken = await _tokenService.CreateAsync(TokenType.ResetPasswordToken);
            if (entity.PasswordChangeTokenId != null)
            {
                _tokenRepository.Delete(entity.PasswordChangeToken);
            }
            entity.PasswordChangeTokenId = resetPasswordToken.Result.Id;
            _repository.Edit(entity);
            await _unitOfWork.CommitAsync();
            return ServiceResult<UserBaseDto, string>.Success(Mapper.Map<UserBaseDto>(entity), resetPasswordToken.Result.BuildEncryptedToken());
        }

        public async Task<ServiceResult<UserBaseDto, string>> GetSelfDeleteTokenAsync(string email)
        {
            var entity = await _repository.Include(x => x.SelfDeleteToken).FirstOrDefaultAsync(x => x.Email == email);
            var selfDeleteToken = await _tokenService.CreateAsync(TokenType.SelfDeleteToken);
            if (entity.SelfDeleteTokenId != null)
            {
                _tokenRepository.Delete(entity.SelfDeleteToken);
            }
            entity.SelfDeleteTokenId = selfDeleteToken.Result.Id;
            _repository.Edit(entity);
            await _unitOfWork.CommitAsync();
            return ServiceResult<UserBaseDto, string>.Success(Mapper.Map<UserBaseDto>(entity), selfDeleteToken.Result.BuildEncryptedToken());
        }

        public async Task<ServiceResult<UserBaseDto>> ResetPasswordAsync(string token, string newPassword)
        {
            var decryptedTokenData = _tokenService.GetDecryptedData(token);
            var entity = await _repository.FirstOrDefaultAsync(x => x.PasswordChangeTokenId == decryptedTokenData.Result.Id);
            if (entity != null && decryptedTokenData.Result.TokenType == TokenType.ResetPasswordToken)
            {
                var newHashedPassword = _passwordHasher.CreateHash(newPassword);
                entity.PasswordSalt = newHashedPassword.Salt;
                entity.PasswordHash = newHashedPassword.Hash;
                _repository.Edit(entity);
                _tokenService.Delete(decryptedTokenData.Result.Id);
                await _unitOfWork.CommitAsync();
                return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(entity));
            }
            return ServiceResult<UserBaseDto>.Failure("Nieważny token zmiany hasła.");
        }

        public async Task<ServiceResult<UserBaseDto>> ChangePasswordAsync(string email, string password, string newPassword)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Email == email);
            if (entity != null && _passwordHasher.ValidatePassword(password, entity.PasswordHash, entity.PasswordSalt))
            {
                var newHashedPassword = _passwordHasher.CreateHash(newPassword);
                entity.PasswordSalt = newHashedPassword.Salt;
                entity.PasswordHash = newHashedPassword.Hash;
                _repository.Edit(entity);
                await _unitOfWork.CommitAsync();
                return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(entity));
            }
            return ServiceResult<UserBaseDto>.Failure("Wystąpił błąd podczas zmiany hasła, spróbuj jeszcze raz.");
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
            return ServiceResult<int>.Success(entity.Charges);
        }

        public async Task<ServiceResult<UserBaseDto>> LoginMvcAsync(string email, string password)
        {
            var stud = await _repository.FirstOrDefaultAsync(x => x.Email == email);
            if (stud != null && _passwordHasher.ValidatePassword(password, stud.PasswordHash, stud.PasswordSalt) && !stud.LockedOut && !stud.IsDeleted)
            {
                return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(stud));
            }
            return ServiceResult<UserBaseDto>.Failure("Niepoprawny login lub hasło");
        }

        //TODO: Token based authentication
        public async Task<ServiceResult<UserBaseDto>> LoginFirstTimeAsync(string email, string password)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResult<UserBaseDto>> CheckLogin(string email, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> CheckHash(string email, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> SelfDeleteAsync(string email, string token)
        {
            var decryptedTokenData = _tokenService.GetDecryptedData(token);
            var entity = await _repository.FirstOrDefaultAsync(x => x.SelfDeleteTokenId == decryptedTokenData.Result.Id);
            if (entity != null && decryptedTokenData.Result.TokenType == TokenType.SelfDeleteToken)
            {
                entity.IsDeleted = true;
                _repository.Edit(entity);
                _tokenService.Delete(decryptedTokenData.Result.Id);
                await _unitOfWork.CommitAsync();
                return ServiceResult<bool>.Success(true);
            }
            return ServiceResult<bool>.Failure("Nieważny token usunięcia konta.");
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

        public async Task<ServiceResult<UserBaseDto>> EditStudentInitialsAsync(UserBaseDto entity)
        {
            var student = await _repository.FirstOrDefaultAsync(x => x.Email == entity.Email);
            student.Name = entity.Name;
            student.LastName = entity.LastName;
            _repository.Edit(student);
            await _unitOfWork.CommitAsync();
            return ServiceResult<UserBaseDto>.Success(Mapper.Map<UserBaseDto>(student));
        }

        public async Task<ServiceResult<int>> TransferCharges(string senderEmail, string recieverEmail, int numberOfCharges, string password)
        {
            var sender = await _repository.FirstOrDefaultAsync(x => x.Email == senderEmail);
            var reciever = await _repository.FirstOrDefaultAsync(x => x.Email == recieverEmail);
            if (sender != null && recieverEmail != null && sender.Charges >= numberOfCharges)
            {
                if (_passwordHasher.ValidatePassword(password, sender.PasswordHash, sender.PasswordSalt))
                {
                    sender.Charges -= numberOfCharges;
                    reciever.Charges += numberOfCharges;
                    _repository.Edit(sender);
                    _repository.Edit(reciever);
                    await _unitOfWork.CommitAsync();
                    return ServiceResult<int>.Success(sender.Charges);
                }
                return ServiceResult<int>.Failure($"Nie można przekazać {numberOfCharges} wyjazdów na konto {recieverEmail} - złe hasło.");
            }
            return ServiceResult<int>.Failure($"Nie można przekazać {numberOfCharges} wyjazdów na konto {recieverEmail}");
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
