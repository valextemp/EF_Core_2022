
using _015_EF_Core_TablePerHierarchy;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("\nВывод данных из БД");
        using (ApplicationContext db = new ApplicationContext())
        {
            var users = db.Users.ToList();
            Console.WriteLine("Users list:");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name} ");
            }
        }
    }
}