using System;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface ITokenService:IEntityService<TokenBaseDto,long>, IDependencyService
    {
        ServiceResult<TokenBaseDto> Create(TokenType tokenType);
        Task<ServiceResult<TokenBaseDto>> CreateAsync(TokenType tokenType);
        ServiceResult<SplittedTokenData> GetDecryptedData(string encryptedData);

        Task<ServiceResult<TokenBaseDto>> GetTokenBySecureTokenAndTypeAsync(Guid secureToken, TokenType type);
        Task<ServiceResult> DeleteTokenBySecureTokenAndTypeAsync(Guid secureToken, TokenType type);
    }
}
