using _003_DB_GRUD_operations;

internal class Program
{
    private static void Main(string[] args)
    {
        // добавление в БД
        using (ApplicationContext db = new ApplicationContext())
        {
            User tom = new User() { Name = "Tom", Age = 33 };
            User alice = new() { Name = "Alice", Age = 26 };

            db.Users.Add(tom);
            db.Users.Add(alice);
            db.SaveChanges();
        }


        using (ApplicationContext db = new ApplicationContext())
        {
            // получаем объекты из бд и выводим на консоль
            var users = db.Users.ToList();
            Console.WriteLine("Данные в БД");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }

        //Редактирование
        using (ApplicationContext db=new() )
        {
            User? user=db.Users.FirstOrDefault<User>();
            if (user != null)
            {
                user.Name = "Bob";
                user.Age = 44;
                db.SaveChanges();

            }
            //выводим данные после обновления

        }

        using (ApplicationContext db = new ApplicationContext())
        {
            // получаем объекты из бд и выводим на консоль
            var users = db.Users.ToList();
            Console.WriteLine("\nДанные в БД после редактирования");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }

        //Удаление
        using (ApplicationContext db = new())
        {
            User? user = db.Users.FirstOrDefault();
            if (user !=null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        using (ApplicationContext db = new ApplicationContext())
        {
            // получаем объекты из бд и выводим на консоль
            var users = db.Users.ToList();
            Console.WriteLine("\nДанные в БД после удаления");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }

        Console.ReadLine();
    }
}