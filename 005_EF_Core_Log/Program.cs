using _005_EF_Core_Log;

internal class Program
{
    private static void Main(string[] args)
    {
        using (ApplicationContext db = new())
        {
            User user1 = new() { Name = "Tom", Age = 33 };
            User user2 = new User() { Name = "Alice", Age = 26 };
            db.Users.Add(user1);    
            db.Users.Add(user2);
            db.SaveChanges();

            var users = db.Users.ToList();
            Console.WriteLine("\nСписок пользователей");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }
    }
}