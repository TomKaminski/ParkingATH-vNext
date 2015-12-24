using System;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Business.Services
{
    public class TokenService : EntityService<TokenBaseDto, Token, long>, ITokenService
    {
        private readonly ITokenRepository _repository;
        private const char DefaultSplitCharacter = '&';

        public TokenService(IUnitOfWork unitOfWork, ITokenRepository repository)
            : base(repository, unitOfWork)
        {
            _repository = repository;
        }


        public ServiceResult<TokenBaseDto> Create(string dataToEncrypt, int daysValid = 7)
        {
            var encryptedData = EncryptHelper.Encrypt(dataToEncrypt);
            return base.Create(new TokenBaseDto
            {
                EncryptedToken = encryptedData,
                ValidTo = DateTime.Now.AddDays(daysValid)
            });
            
        }

        public ServiceResult<string[]> GetDecryptedData(long id, char splitCharacter = DefaultSplitCharacter)
        {
            var tokenFromId = _repository.Find(id);
            var decryptedData = EncryptHelper.Decrypt(tokenFromId.EncryptedToken);
            var splitted = decryptedData.Split(new[] { splitCharacter }, StringSplitOptions.RemoveEmptyEntries);
            return ServiceResult<string[]>.Success(splitted);
        }

        public ServiceResult<string[]> GetDecryptedData(string encryptedData, char splitCharacter = DefaultSplitCharacter)
        {
            var decryptedData = EncryptHelper.Decrypt(encryptedData);
            var splitted = decryptedData.Split(new[] {splitCharacter}, StringSplitOptions.RemoveEmptyEntries);
            return ServiceResult<string[]>.Success(splitted);
        }
    }
}
