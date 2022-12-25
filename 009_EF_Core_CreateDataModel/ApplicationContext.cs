using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace _009_EF_Core_CreateDataModel
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        //public DbSet<User> Users => Set<User>(); //Другое решение - инициализировать свойство значением типа Set<T>: (чтобы не использовать null!)
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated(); // Будет ошибка если использовать миграции
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=helloapp.db");

            //Не получается сделать БД в тойже папке
            //string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "helloapp1.db");
            //optionsBuilder.UseSqlite($"FileName={path}");

            //Поэтому забиваю путь константой
            string path = @"D:\0_valexproject\00_Metanit\EF_Core_2022\009_EF_Core_CreateDataModel\helloapp.db";
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Использование Fluent API
            modelBuilder.Entity<Country>();
            modelBuilder.Entity<User>().Ignore(u=>u.Address);
        }

    }
}
