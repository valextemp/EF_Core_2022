using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace _018_EF_Core_CompileQuery
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=helloapp.db");

            //Не получается сделать БД в тойже папке
            //string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "helloapp1.db");
            //optionsBuilder.UseSqlite($"FileName={path}");

            //Поэтому забиваю путь константой
            string path = @"D:\0_valexproject\00_Metanit\EF_Core_2022\018_EF_Core_CompileQuery\helloapp.db";
            optionsBuilder.UseSqlite($"Data Source={path}");

            //Ниже строка для MSSql Server
            //Только при указании TrustServerCertificate = True - заработало
            //optionsBuilder.UseSqlServer(@"Server=192.168.192.10;Database=helloapp;User Id=sa; Password=Avv-74176;TrustServerCertificate=True");           
        }
    }
}
