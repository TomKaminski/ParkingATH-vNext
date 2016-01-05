using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.AdminPriceTreshold;
using ParkingATHWeb.Contracts.DTO.PriceTreshold;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminPriceTresholdController : AdminServiceController
                                                <AdminPriceTresholdListItemViewModel, 
                                                 AdminPriceTresholdCreateViewModel, 
                                                 AdminPriceTresholdEditViewModel, 
                                                 AdminPriceTresholdDeleteViewModel, 
                                                 PriceTresholdBaseDto, int>
    {
        public AdminPriceTresholdController(IEntityService<PriceTresholdBaseDto, int> entityService) : base(entityService)
        {
        }
    }
}
