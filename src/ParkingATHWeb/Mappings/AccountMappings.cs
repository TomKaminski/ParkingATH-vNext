using AutoMapper;
using ParkingATHWeb.Areas.Portal.ViewModels.Account;
using ParkingATHWeb.Areas.Portal.ViewModels.Manage;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Models;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Mappings
{
    public static partial class FrontendMappingsProvider
    {
        private static void InitAccountMappings()
        {
            Mapper.CreateMap<RegisterViewModel, UserBaseDto>()
                .ForMember(x => x.Charges, opt => opt.UseValue(0))
                .ForMember(x => x.IsAdmin, opt => opt.UseValue(false))
                .ForMember(x => x.LockedOut, opt => opt.UseValue(false))
                .ForMember(x => x.UnsuccessfulLoginAttempts, opt => opt.UseValue(0))
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<LoginViewModel, UserBaseDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<UserBaseDto, AppUserState>().IgnoreNotExistingProperties();

            Mapper.CreateMap<ChangeUserInfoViewModel, UserBaseDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<UserBaseDto, ChangeUserInfoViewModel>().IgnoreNotExistingProperties();
        }
    }
}
