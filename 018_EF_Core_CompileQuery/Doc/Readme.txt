Скомпилированные запросы

Функциональность скомпилированных запросов позволяют приложению кэшировать запрос в форму, понятную для источника данных. 
Поэтому достаточно один раз создать такой запрос и затем выполнять его много раз, что повышает производительность приложения.

Хотя EF Core может автоматически компилировать и кэшировать запросы на основании хэш-представления выражений LINQ, тем не менее компилируемые запросы позволяют 
хоть немножно но повысить производительность, так как в этом случае не вычисляется хэш-значение и не производится поиск в кэше, а приложение может использовать 
уже скомпилированные запросы через вызов делегатов.

Для компиляции запроса набор выражений LINQ передается в метод EF.CompileQuery(), а скомпилированный запрос затем присваивается делегату:

делегат = EF.CompileQuery((параметры) => выражения_LINQ)

Func<ApplicationContext, int, User?> userById =
    EF.CompileQuery((ApplicationContext db, int id) =>
         db.Users.Include(c => c.Company).FirstOrDefault(c => c.Id == id));

    var user = userById(db, 1);



Func<ApplicationContext, string, int, IEnumerable<User>> usersByNameAndAge =
    EF.CompileQuery((ApplicationContext db, string name, int age) =>
        db.Users.Include(c => c.Company)
                 .Where(u=>EF.Functions.Like(u.Name!, name) && u.Age>age));

    var users = usersByNameAndAge(db, "%Tom%", 30).ToList();