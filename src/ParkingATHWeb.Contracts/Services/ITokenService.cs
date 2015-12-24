using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface ITokenService:IEntityService<TokenBaseDto,long>, IDependencyService
    {
        ServiceResult<TokenBaseDto> Create(TokenType tokenType);
        ServiceResult<string[]> GetDecryptedData(string encryptedData);
    }
}
