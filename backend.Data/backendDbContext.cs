using Microsoft.EntityFrameworkCore;
using System;

namespace backend.Data
{
  public class backendDbContext : DbContext
  {
    public backendDbContext(DbContextOptions options) : base(options)
    {

    }

    
  }
}
