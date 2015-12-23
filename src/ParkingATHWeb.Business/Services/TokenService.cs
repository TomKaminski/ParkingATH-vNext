using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.Business.Services
{
    public class TokenService : EntityService<TokenBaseDto, Token, long>, ITokenService
    {
        private readonly ITokenRepository _repository;

        public TokenService(IUnitOfWork unitOfWork, ITokenRepository repository)
            : base(repository, unitOfWork)
        {
            _repository = repository;
        }
    }
}
