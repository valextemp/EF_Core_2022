using _016_EF_Core_ConcurrencyCheck;

internal class Program
{
    private static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            User mike = new User() { Name = "Mike",Age=44 };
            User alice = new() { Name = "Alice", Age=55 };

            db.Users.Add(mike);
            db.Users.Add(alice);
            db.SaveChanges();
        }


        using (ApplicationContext db = new ApplicationContext())
        {
            // получаем объекты из бд и выводим на консоль
            var users = db.Users.ToList();
            Console.WriteLine("\nДанные в БД после добавления");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - Age = { u.Age}");
            }
        }

        //Редактирование
        using (ApplicationContext db = new())
        {
            User? user = db.Users.FirstOrDefault(u => u.Id == 1);
            if (user != null)
            {
                //user.Name = "Bob";
                user.Age = 77;
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
                Console.WriteLine($"{u.Id}.{u.Name} - Age = {u.Age}");
            }
        }

    }
}