using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace _013_EF_Core_ForeignKey_FluentAPI
{
    public class ApplicationContex:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Company> Companies { get; set; } = null!;
        public ApplicationContex()
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
            string path = @"D:\0_valexproject\00_Metanit\EF_Core_2022\013_EF_Core_ForeignKey_FluentAPI\helloapp.db";
            optionsBuilder.UseSqlite($"Data Source={path}");

            // Ниже строка для MSSql Server
            // Только при указании TrustServerCertificate=True - заработало
            //optionsBuilder.UseSqlServer(@"Server=192.168.192.10;Database=helloapp;User Id=sa; Password=Avv-74176;TrustServerCertificate=True");            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.Company).WithMany(c => c.Users).HasForeignKey(u => u.CompanyInfoKey);
        }
    }
}
