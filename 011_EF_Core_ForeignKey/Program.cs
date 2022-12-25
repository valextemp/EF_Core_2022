using _011_EF_Core_ForeignKey;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("\nВнесение данных в БД");
        using (ApplicationContext db = new ApplicationContext())
        {
            Company company1 = new() { Name = "Google" };
            Company company2 = new Company() { Name = "Microsoft" };
            User user1 = new User() { Name = "Tom", Age = 22, Company = company1 };
            User user2 = new() { Name = "Bob", Age = 33, Company = company2 };
            User user3 = new() { Name = "Sam", Age = 44, Company = company2 };

            db.Companys.AddRange(company1, company2);
            db.Users.AddRange(user1, user2, user3);
            db.SaveChanges();

            //foreach (var user in db.Users.ToList())
            //{
            //    Console.WriteLine($"{user.Name} работает в {user.Company?.Name}");
            //}
        }


        Console.WriteLine("\nВывод данных из БД");
        using (ApplicationContext db = new ApplicationContext())
        {
            var users = db.Users.ToList();
            Console.WriteLine("Users list:");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name} работает в {user.Company?.Name} и его возраст = {user.Age}");
            }
        }

        //Также можно использовать свойство-внешний ключ для установки связи:
        //using (ApplicationContext db = new ApplicationContext())
        //{
        //    Company company1 = new Company { Name = "Google" };
        //    Company company2 = new Company { Name = "Microsoft" };
        //    db.Companys.AddRange(company1, company2);  // добавление компаний
        //    db.SaveChanges();

        //    User user1 = new User { Name = "Tom", CompanyId = company1.Id };
        //    User user2 = new User { Name = "Bob", CompanyId = company1.Id };
        //    User user3 = new User { Name = "Sam", CompanyId = company2.Id };

        //    db.Users.AddRange(user1, user2, user3);     // добавление пользователей
        //    db.SaveChanges();

        //    foreach (var user in db.Users.ToList())
        //    {
        //        Console.WriteLine($"{user.Name} работает в {user.Company?.Name}");
        //    }
        //}

        //Выше для установки связи применялась зависимая сущность - User. Но мы также можем зайти с другой стороны и установить набор зависимых сущностей
        //через навигационное свойство главной сущности:
        //using (ApplicationContext db = new ApplicationContext())
        //{
        //    User user1 = new User { Name = "Tom" };
        //    User user2 = new User { Name = "Bob" };
        //    User user3 = new User { Name = "Sam" };

        //    Company company1 = new Company { Name = "Google", Users = { user1, user2 } };
        //    Company company2 = new Company { Name = "Microsoft", Users = { user3 } };

        //    db.Companys.AddRange(company1, company2);  // добавление компаний
        //    db.Users.AddRange(user1, user2, user3);     // добавление пользователей
        //    db.SaveChanges();

        //    foreach (var user in db.Users.ToList())
        //    {
        //        Console.WriteLine($"{user.Name} работает в {user.Company?.Name}");
        //    }
        //}

    }
}