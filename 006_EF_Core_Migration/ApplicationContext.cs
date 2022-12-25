using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace _006_EF_Core_Migration
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureCreated(); // Будет ошибка если использовать миграции
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=helloapp.db");

            //Не получается сделать БД в тойже папке
            //string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "helloapp1.db");
            //optionsBuilder.UseSqlite($"FileName={path}");

            //Поэтому забиваю путь константой
            string path = @"D:\0_valexproject\00_Metanit\EF_Core_2022\006_EF_Core_Migration\helloapp2.db";
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

    }
}
