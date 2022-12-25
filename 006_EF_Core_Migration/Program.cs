using _006_EF_Core_Migration;
internal class Program
{
    private static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            User tom = new User() { Name = "Mike", Age = 45 };
            User alice = new() { Name = "Michel", Age = 35 };

            db.Users.Add(tom);
            db.Users.Add(alice);
            db.SaveChanges();
        }


        using (ApplicationContext db = new ApplicationContext())
        {
            var users=db.Users.ToList();
            Console.WriteLine("Список пользователей:");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }
    }
}