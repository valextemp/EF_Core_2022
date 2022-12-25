
С помощью атрибутов и Fluent API для сущостей и их свойств можно установить многочисленные настройки. Однако, если настроек очень много, то они могут утяжелять класс контекста и сущностей. 
В этом случае Entity Framework Core позволяет вынести конфигурацию сущностей в отдельные классы.

Для вынесения конфигурации во вне необходимо создать класс конфигурации, реализующий интерфейс IEntityTypeConfiguration<T>.

    -- IEntityTypeConfiguration<T>
        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable("People").Property(p => p.Name).IsRequired();
                builder.Property(p => p.Id).HasColumnName("user_id");
            }
        }

    -- В классе контекста
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

    -- Отдельные методы в контексте данных
    В качестве альтернативы мы могли бы использовать еще один вариант. Вместо выделения отдельных классов конфигураций определить конфигурацию 
    в виде отдельных методов в том же классе контекста данных:

        public void UserConfigure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("People").Property(p => p.Name).IsRequired();
            builder.Property(p => p.Id).HasColumnName("user_id");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(UserConfigure);
            modelBuilder.Entity<Company>(CompanyConfigure);
        }

    -- Атрибут EntityTypeConfiguration
        Еще один альтернативный вариант применения конфигураций представляет атрибут EntityTypeConfiguration, который применяется к сущности и который получает тип класса конфигурации:

        using Microsoft.EntityFrameworkCore;
        using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
        public class ApplicationContext : DbContext
        {
            public DbSet<User> Users { get; set; } = null!;
            public DbSet<Company> Companies { get; set; } = null!;
            public ApplicationContext()
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=helloapp.db");
            }
        }
        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable("People").Property(p => p.Name).IsRequired();
                builder.Property(p => p.Id).HasColumnName("user_id");
 
            }
        }
        public class CompanyConfiguration : IEntityTypeConfiguration<Company>
        {
            public void Configure(EntityTypeBuilder<Company> builder)
            {
                builder.ToTable("Enterprises").Property(c => c.Name).IsRequired();
            }
        }
        [EntityTypeConfiguration(typeof(UserConfiguration))]
        public class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Age { get; set; }
        }
        [EntityTypeConfiguration(typeof(CompanyConfiguration))]
        public class Company
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }

-- Инициализация БД начальными данными
    Для инициализации БД при конфигурации определенной модели вызывается метод HasData(), в который передаются добавляемые данные:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData( new User { Id=1, Name="Tom", Age=36});
        }