using System;
using Microsoft.Data.Entity;

namespace ParkingATHWeb.DataAccess
{
    public interface IDatabaseFactory:IDisposable
    {
        DbContext Get();
    }
}
