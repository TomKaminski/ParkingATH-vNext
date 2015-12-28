using AutoMapper;
using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public static partial class BackendMappingProvider
    {
        private static void InitializeTokenMappings()
        {
            Mapper.CreateMap<Token, TokenBaseDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<TokenBaseDto, Token>().IgnoreNotExistingProperties();

            Mapper.CreateMap<SplittedTokenData, TokenBaseDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<TokenBaseDto, SplittedTokenData>().IgnoreNotExistingProperties();
        }
    }
}
