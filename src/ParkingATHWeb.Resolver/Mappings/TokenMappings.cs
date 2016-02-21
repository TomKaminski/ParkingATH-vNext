using AutoMapper;
using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public class TokenBackendMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<Token, TokenBaseDto>().IgnoreNotExistingProperties();

            CreateMap<TokenBaseDto, Token>().IgnoreNotExistingProperties();

            CreateMap<SplittedTokenData, TokenBaseDto>().IgnoreNotExistingProperties();

            CreateMap<TokenBaseDto, SplittedTokenData>().IgnoreNotExistingProperties();
        }
    }
}
