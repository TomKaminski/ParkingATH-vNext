using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface ITokenService:IEntityService<TokenBaseDto,long>, IDependencyService
    {
    }
}
