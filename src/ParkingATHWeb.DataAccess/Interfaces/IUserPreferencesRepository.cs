﻿using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Interfaces
{
    public interface IUserPreferencesRepository : IGenericRepository<UserPreferences, int>, IDependencyRepository
    {
    }
}
