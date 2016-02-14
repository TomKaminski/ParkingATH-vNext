using System;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using ParkingATHWeb.Business.Providers;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Business.Services
{
    public class TokenService : EntityService<TokenBaseDto, Token, long>, ITokenService
    {
        private readonly ITokenRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(IUnitOfWork unitOfWork, ITokenRepository repository)
            : base(repository, unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public ServiceResult<TokenBaseDto> Create(TokenType tokenType)
        {
            return base.Create(GetTokenToAdd(tokenType));
        }

        public async Task<ServiceResult<TokenBaseDto>> CreateAsync(TokenType tokenType)
        {
            return await base.CreateAsync(GetTokenToAdd(tokenType));
        }

        public ServiceResult<SplittedTokenData> GetDecryptedData(string encryptedData)
        {
            var decryptedData = EncryptHelper.Decrypt(encryptedData);
            var tokenDto = JsonConvert.DeserializeObject<TokenBaseDto>(decryptedData);
            return ServiceResult<SplittedTokenData>.Success(Mapper.Map<SplittedTokenData>(tokenDto));
        }

        public async Task<ServiceResult<TokenBaseDto>> GetTokenBySecureTokenAndTypeAsync(Guid secureToken, TokenType type)
        {
            var token = Mapper.Map<TokenBaseDto>(await _repository.FirstOrDefaultAsync(x => x.TokenType == type && x.SecureToken == secureToken));
            if (token == null)
            {
                return ServiceResult<TokenBaseDto>.Failure("Not found");
            }
            return token.NotExpired()
                ? ServiceResult<TokenBaseDto>.Success(token)
                : ServiceResult<TokenBaseDto>.Failure("Expired");
        }

        public async Task<ServiceResult> DeleteTokenBySecureTokenAndTypeAsync(Guid secureToken, TokenType type)
        {
            var token = await _repository.FirstOrDefaultAsync(x => x.TokenType == type && x.SecureToken == secureToken);
            _repository.Delete(token);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        private static TokenBaseDto GetTokenToAdd(TokenType tokenType)
        {
            return new TokenBaseDto
            {
                SecureToken = Guid.NewGuid(),
                TokenType = tokenType,
                ValidTo = TokenValidityTimeProvider.GetValidToDate(tokenType)
            };
        }
    }
}
