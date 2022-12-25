
using _010_EF_Core_ConfigDataModel;
internal class Program
{
    private static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            // получаем объекты из бд и выводим на консоль
            var users = db.Users.ToList();
            Console.WriteLine("Users list:");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }
        }
    }
}