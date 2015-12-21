using Microsoft.Data.Entity;
using ParkingATHWeb.Model;

namespace ParkingATHWeb.DataAccess
{
    public class DatabaseFactory:IDatabaseFactory
    {
        private ParkingAthContext _context;
        private bool _disposed;

        public DatabaseFactory(ParkingAthContext context)
        {
            _context = context;
        }
        public DbContext Get()
        {
            return _context ?? (_context = new ParkingAthContext());
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
                _context.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
