using System;
using System.Threading.Tasks;

namespace ParkingATHWeb.DataAccess.Common
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
