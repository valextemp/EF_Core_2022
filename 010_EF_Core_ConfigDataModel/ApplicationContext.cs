using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _010_EF_Core_ConfigDataModel
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=helloapp.db");

            //Не получается сделать БД в тойже папке
            //string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "helloapp1.db");
            //optionsBuilder.UseSqlite($"FileName={path}");

            //Поэтому забиваю путь константой
            string path = @"D:\0_valexproject\00_Metanit\EF_Core_2022\010_EF_Core_ConfigDataModel\helloapp.db";
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());

            modelBuilder.Entity<Country>(CountryConfiguring); //Еще вариант см метод ниже

            //Первичная инициализация данными
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Tom", Age = 23 },
                new User { Id = 2, Name = "Alice", Age = 26 },
                new User { Id = 3, Name = "Sam", Age = 28 }
        );
        }

        public void CountryConfiguring(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.Id).HasColumnName("country_id");
        }

    }
}
