using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

using Microsoft.EntityFrameworkCore;
using _004_EF_Core_cfg_file;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();

        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");

        var config=builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        var options = optionsBuilder.UseSqlite(connectionString).Options;

        using (ApplicationContext db=new(options))
        {
            var users = db.Users.ToList();
            foreach (User user in users)
                Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
        }


    }
}