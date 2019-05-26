using backendData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace backendData
{
    public static class backendDbSeedData
    {
        public static void EnsureSeedData(this backendDbContext db)
        {
            if ( !db.Users.Any() )
            {
                var users = new List<User>
                {
                    new User { Email = "eoinom@gmail.com", Password = "pass123", FirstName = "Eoin", LastName = "O'Malley" },
                    new User { Email = "joe.bloggs@gmail.com", Password = "pass123", FirstName = "Joe", LastName = "Bloggs" },
                    new User { Email = "paulmcc@applerecords.com", Password = "pass123", FirstName = "Paul", LastName = "McCartney" },
                    new User { Email = "john@imagine.com", Password = "pass123", FirstName = "John", LastName = "Lennon" },
                    new User { Email = "george@altavista.com", Password = "pass123", FirstName = "George", LastName = "Harrisson" },
                    new User { Email = "ringo@drummers4hire.com", Password = "pass123", FirstName = "Ringo", LastName = "Starr" }
                };

                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }
    }
}
