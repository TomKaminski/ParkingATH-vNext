﻿using System;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Interfaces
{
    public interface IPortalMessageRepository:IGenericRepository<PortalMessage,Guid>, IDependencyRepository
    {
    }
}
