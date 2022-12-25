using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace _008_EF_Core_PostgreSQL_provider
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
            optionsBuilder.UseNpgsql(@"Host=192.168.192.45;Port=5432;Database=helloapp;Username=postgres;Password=Avv-74176");
        }
    }
}
