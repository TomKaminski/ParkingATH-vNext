using System;
using AutoMapper;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.DTO.SupportMessage;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.Business.Services
{
    public class PortalMessageService : EntityService<PortalMessageDto, PortalMessage, Guid>, IPortalMessageService
    {
        public PortalMessageService(IGenericRepository<PortalMessage, Guid> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
        }
    }
}
