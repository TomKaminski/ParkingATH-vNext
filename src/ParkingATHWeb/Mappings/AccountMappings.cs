using AutoMapper;
using ParkingATHWeb.Areas.Portal.ViewModels.Account;
using ParkingATHWeb.Areas.Portal.ViewModels.Manage;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Models;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Mappings
{
    public class AccountFrontendMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<RegisterViewModel, UserBaseDto>()
              .ForMember(x => x.Charges, opt => opt.UseValue(0))
              .ForMember(x => x.IsAdmin, opt => opt.UseValue(false))
              .ForMember(x => x.LockedOut, opt => opt.UseValue(false))
              .ForMember(x => x.UnsuccessfulLoginAttempts, opt => opt.UseValue(0))
              .IgnoreNotExistingProperties();

            CreateMap<LoginViewModel, UserBaseDto>().IgnoreNotExistingProperties();

            CreateMap<UserBaseDto, AppUserState>().IgnoreNotExistingProperties();

            CreateMap<ChangeUserInfoViewModel, UserBaseDto>().IgnoreNotExistingProperties();
            CreateMap<UserBaseDto, ChangeUserInfoViewModel>().IgnoreNotExistingProperties();
        }
    }
}
