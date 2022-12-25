using _018_EF_Core_CompileQuery;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    // при такой записи static пришлось добавить
    static Func<ApplicationContext, int, User?> UserById = EF.CompileQuery((ApplicationContext db, int id) => db.Users.Include(c => c.Company).FirstOrDefault(c => c.Id == id));
    static Func<ApplicationContext, string,int, IEnumerable<User>> usersByNameAndAge = EF.CompileQuery((ApplicationContext db, string name, int age) =>
                                    db.Users.Include(c => c.Company)
                                            .Where(u => EF.Functions.Like(u.Name!, name) && u.Age > age));

    //Func<ApplicationContext, string, int, IEnumerable<User>> usersByNameAndAge =
    //    EF.CompileQuery((ApplicationContext db, string name, int age) =>
    //            db.Users.Include(c => c.Company)
    //                    .Where(u => EF.Functions.Like(u.Name!, name) && u.Age > age));

    private static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            // пересоздаем базу данных
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Company microsoft = new Company { Name = "Microsoft" };
            Company google = new Company { Name = "Google" };
            db.Companies.AddRange(microsoft, google);

            User tom = new User { Name = "Tom", Age = 36, Company = microsoft };
            User bob = new User { Name = "Bob", Age = 39, Company = google };
            User alice = new User { Name = "Alice", Age = 28, Company = microsoft };
            User kate = new User { Name = "Kate", Age = 25, Company = google };
            User tomas = new User { Name = "Tomas", Age = 30, Company = microsoft };
            User tomek = new User { Name = "Tomek", Age = 42, Company = google };

            db.Users.AddRange(tom, bob, alice, kate, tomas, tomek);
            db.SaveChanges();
        }

        using (ApplicationContext db = new ApplicationContext())
        {
            var user = UserById(db, 1);
            if (user != null) Console.WriteLine($"{user.Name} - {user.Age} \n");

            var users = usersByNameAndAge(db, "%Tom%", 30).ToList();
            foreach (var u in users)
                Console.WriteLine($"{u.Name} - {u.Age}");
        }
    }
}