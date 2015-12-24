using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface ITokenService:IEntityService<TokenBaseDto,long>, IDependencyService
    {
        ServiceResult<TokenBaseDto> Create(string dataToEncrypt, int daysValid);
        ServiceResult<string[]> GetDecryptedData(long id, char splitCharacter);
        ServiceResult<string[]> GetDecryptedData(string encryptedData, char splitCharacter);
    }
}
