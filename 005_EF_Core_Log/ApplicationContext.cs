using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace _005_EF_Core_Log
{
    public class ApplicationContext : DbContext
    {
        // Для логирования в файл
        readonly StreamWriter logStream = new StreamWriter("mylog.txt",true);
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
            //optionsBuilder.LogTo(Console.WriteLine); // Логирование в консоль, только одна строчка кода в этом методе

            //  логгирование в окно Output, что производится с помощью метода Debug.WriteLine():, только одна строчка кода в этом методе
            //optionsBuilder.LogTo(message=>System.Diagnostics.Debug.WriteLine(message));

            // логирование в файл требует больше кода см logStream выше
            //optionsBuilder.LogTo(logStream.WriteLine); //логирует все
            //optionsBuilder.LogTo(logStream.WriteLine, new[] { RelationalEventId.CommandExecuted }); // только SQL команды буду логировать

            //выведем в лог информацию только об исполняемых командах:
            optionsBuilder.LogTo(logStream.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });

        }

        public override void Dispose()
        {
            base.Dispose(); 
            logStream.Dispose();    
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await logStream.DisposeAsync();
        }
    }
}
