using Core.Entities;
using Core.Helper;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DbInitializer
    {
        static int id;
        public static void SeedAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin
                {
                    Id = id++,
                    Username = "admin1",
                    Password = PasswordHasher.Encrypt("021196"),
                    CreatedBy = " System "
                },
               new Admin
                {
                    Id = id++,
                    Username = "admin2",
                    Password = PasswordHasher.Encrypt("101297"),
                    CreatedBy = " System "
                }

            };
            DbContext.Admins.AddRange(admins);
        }

    }
}
