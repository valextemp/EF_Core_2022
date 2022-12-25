
using _017_EF_Core_Logger;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Создаю и записываю в БД\n");
        using (ApplicationContext db = new ApplicationContext())
        {
            db.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            User user1 = new() { Name = "Tom", Age = 33 };
            User user2 = new User() { Name = "Alice", Age = 26 };

            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();
        }

        Console.WriteLine("Повторное считывание из БД\n");
        using (ApplicationContext db = new ApplicationContext())
        {
            //db.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            var users = db.Users.ToList();
            Console.WriteLine("Список пользователей:");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }
    }
}