using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace _007_EF_Core_MSSQL_provider
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloapp;Trusted_Connection=True;");

            //optionsBuilder.UseSqlServer(@"Server=192.168.192.10;Database=helloapp;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(@"Server=192.168.192.10;Database=helloapp;User Id=sa; Password=Avv-74176");

            // Только при указании TrustServerCertificate=True - заработало
            optionsBuilder.UseSqlServer(@"Server=192.168.192.10;Database=helloapp;User Id=sa; Password=Avv-74176;TrustServerCertificate=True");

        }
    }
}
