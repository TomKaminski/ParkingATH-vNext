using System;
using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class MessageRepository : GenericRepository<Message, Guid>, IMessageRepository
    {
        private readonly DbSet<Message> _dbset;

        public MessageRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbset = factory.Get().Set<Message>();
        }
    }
}
