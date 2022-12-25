
Microsoft.EntityFrameworkCore.Tools
Для использования миграций в Visual Stuido необходимо добавить в проект через менеджер Nuget пакет Microsoft.EntityFrameworkCore.Tools.

Вариант определения пути
    string path=System.IO.Path.Combine(System.Environment.CurrentDirectory, "Northwind.db");
    optionsBuilder.UseSqlite($"FileName={path}");

    optionsBuilder.UseSqlite("Filename=../Northwind.db");

    optionsBuilder.UseSqlite("Data Source=helloapp.db");

---============================================
Для создания скрипта sql необходимо ввести в окне Package Manager Console команду --  Script-Migration

акже можно передать название миграции, по которой необходимо создать скрипт: Script-Migration InitialCreate

---============================================
Команды в VS Code
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef migrations script
dotnet ef migrations script InitialCreate

--============================================
Если миграции создаются при наличии файла конфигурации appsettings.json(из которого получаем строку подключения):
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=D:\helloapp2.db"
  }
}
При создании миграций будет ошибка
В этом случае при выполнении миграции инструментарий Entity Frameworkа ищет класс, который реализует интерфейс IDesignTimeDbContextFactory и 
который задает конфигурацию контекста.

Поэтому в этом случае нам необходимо добавить в проект подобный класс. Например:
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
 
public class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
 
        // получаем конфигурацию из файла appsettings.json
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();
 
        // получаем строку подключения из файла appsettings.json
        string connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlite(connectionString);
        return new ApplicationContext(optionsBuilder.Options);
    }
}

--===================================================
Объединение миграций
Начиная с версии 6.0 Entity Framework позволяет создавать бандлы миграций - объединение миграций в виде исполняемого файла. 
Для создания бандла миграций надо в Visual Studio в окне Package Manager Console выполнить команду:
        Bundle-Migration
        dotnet ef migrations bundle (.NET CLI)
После выполнения этих команд в папке решения будет сгенерирован файл efbundle (в Windows он будет иметь расширение exe). Запустим его.
И после запуска бандла будут последовательно применяться добавленные в бандл миграции.
