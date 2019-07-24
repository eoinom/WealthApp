using backendData;
using Microsoft.EntityFrameworkCore;
using System;

namespace backendDataAccess.Tests.Infrastructure
{
    public class backendDataAccessTestBase : IDisposable
    {
        protected readonly backendDbContext _context;

        public backendDataAccessTestBase()
        {
            var options = new DbContextOptionsBuilder<backendDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new backendDbContext(options);

            _context.Database.EnsureCreated();

            backendTestDbInitialiser.EnsureSeedData(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();

            _context.Dispose();
        }
    }
}
