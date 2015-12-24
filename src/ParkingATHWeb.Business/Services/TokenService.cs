using System;
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
        private const char DefaultSplitCharacter = '&';

        private const int UserEmailPosition = 0;
        private const int TokenTypePosition = 1;
        private const int SecureTokenPosition = 2;

        public TokenService(IUnitOfWork unitOfWork, ITokenRepository repository)
            : base(repository, unitOfWork)
        {
            _repository = repository;
        }


        public ServiceResult<TokenBaseDto> Create(TokenType tokenType)
        {
            return base.Create(new TokenBaseDto
            {
                TokenType = tokenType,
                ValidTo = TokenValidityTimeProvider.GetValidToDate(tokenType),
                SecureToken = Guid.NewGuid()
            });
        }

        public ServiceResult<string[]> GetDecryptedData(string encryptedData)
        {
            var decryptedData = EncryptHelper.Decrypt(encryptedData);
            var splitted = decryptedData.Split(new[] {DefaultSplitCharacter}, StringSplitOptions.RemoveEmptyEntries);
            return ServiceResult<string[]>.Success(splitted);
        }
    }
}
