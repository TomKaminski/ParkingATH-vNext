using System;
using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class PortalMessageRepository : GenericRepository<PortalMessage, Guid>, IPortalMessageRepository
    {
        private readonly DbSet<PortalMessage> _dbset;

        public PortalMessageRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbset = factory.Get().Set<PortalMessage>();
        }
    }
}
