
-- Механизмы !!*Fluent API*!! и !!*аннотации данных*!! позволяют добавить дополнительные правила конфигурации, либо переопределить используемые условности.

-- Fluent API представляет набор методов, которые определяют сопоставление между классами и их свойствами и таблицами и их столбцами. 
Для использования функционала Fluent API переопределяется метод !!*OnModelCreating()*!!:

-- Аннотации представляют настройку классов сущностей с помощью атрибутов. 
Большинство подобных атрибутов располагаются в пространстве System.ComponentModel.DataAnnotations, которое нам надо подключить перед использованием аннотаций.

-- Таким образом, мы можем использовать три подхода к определению модели:
	-- Условности (conventions)
	-- Fluent API
	-- Аннотации данных

Еще один способ включения сущности в модель представляет вызов Entity() объекта ModelBuilder в методе OnModelCreating():

-- Исключение из модели
	Применение Fluent API заключается в вызове метода Ignore(): (modelBuilder.Ignore<Company>();)
	Аннотации данных предполагают установку над классом атрибута [NotMapped]:
		[NotMapped]
		public class Company
			{
				public int Id { get; set; }
				public string Name { get; set; }
			}

-- Исключение определенного свойства Сущности (чтобы для него не создавался столбец в таблице)
	- Исключение с помощью Fluent API производится через метод Ignore() в методе OnModelCreating() Контекста:
		- modelBuilder.Entity<User>().Ignore(u=>u.Address);
	- Исключение с помощью аннотаций данных:
		- [NotMapped]
		  public string? Address { get; set; }

-- Сопоставление таблиц
	- Атрибут [Table] позволяет переопределить сопоставление с таблицей по имени:
		- [Table("People")]
		  public class User
	- Метод ToTable (Аналогичное переопределение можно произвести через Fluent API с помощью метода ToTable():)
		modelBuilder.Entity<User>().ToTable("People"); (в методе OnModelCreating())
	- С помощью дополнительного параметра schema можно определить схему, к которой будет принадлежать таблица:
		modelBuilder.Entity<User>().ToTable("People", schema: "userstore");

-- Сопоставление столбцов
	- Атрибут Column
		[Column("user_id")]
		public int Id { get; set; } (Теперь свойство Id будет сопоставляться со столбцом "user_id".)
	- Метод HasColumnName
		Также сопоставление можно переопределить в Fluent API с помощью метода HasColumnName:
		- modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("user_id");


-- Обязательные и необязательные свойства
По умолчанию свойство является необязательным к установке, если оно допускает значение null. Это свойства, которые представляют nullable-типы, 
например, string?, int? и т.д. Хотя мы также можем настроить эти свойства как обязательные.	
	-- Атрибут Required 
		Атрибут Required указывает, что данное свойство обязательно для установки, то есть будет иметь определение NOT NULL в БД, 
		даже если оно представляет nullable-тип:
			[Required]
			public string? Name { get; set; }
	-- Метод IsRequired
		То же самое можно сделать и через Fluent API с помощью метода IsRequired():
			modelBuilder.Entity<User>().Property(b => b.Name).IsRequired();
			

-- Настройка ключей
	-- По умолчанию в качестве ключа используется свойство, которое называется Id или [имя_класса]Id. Например:
		- public int Id { get; set; } // или public int UserId { get; set; }
	-- Для установки свойства в качестве первичного ключа с помощью аннотаций применяется атрибут [Key]:
		- [Key]
		- public int Ident { get; set; }		
	-- Для конфигурации ключа с Fluent API применяется метод HasKey():
		- modelBuilder.Entity<User>().HasKey(u => u.Ident);
	-- Дополнительно с помощью Fluent API можно настроить имя ограничения, которое задается для первичного ключа. Для этого применяется метод HasName():
		- modelBuilder.Entity<User>().HasKey(u => u.Ident).HasName("UsersPrimaryKey");

	-- Составные ключи
		- С помощью Fluent API можно создать составной ключ из нескольких свойств (Составной ключ можно создать только с помощью Fluent API):
			modelBuilder.Entity<User>().HasKey(u => new { u.PassportSeria, u.PassportNumber});

	-- Альтернативные ключи
		- Альтернативные ключи представляют свойства, которые также, как и первичный ключ, должны иметь уникальное значение. 
		В то же время альтернативные ключи не являются первичными. На уровне базы данных это выражается в установке для соответствующих столбцов 
		ограничения на уникальность.
			- Для установки альтернативного ключа используется метод HasAlternateKey():
				modelBuilder.Entity<User>().HasAlternateKey(u => u.Passport);
			- Альтернативные ключи также могут быть составными:
				modelBuilder.Entity<User>().HasAlternateKey(u => new { u.Passport, u.PhoneNumber });

-- Настройка индексов
	- Настройка индексов с помощью атрибутов
		- Для создания индекса можно использовать атрибут [Index]. Например:
			[Index("PhoneNumber")] // Или несколько полей [Index("PhoneNumber", "Passport")]
			public class User
			{ ....
			    public string? PhoneNumber { get; set; }		
		- С помощью дополнительных параметров можно настроить уникальность и имя индекса:
				[Index("PhoneNumber", IsUnique = true, Name ="Phone_Index")]

	- Настройка индексов с помощью Fluent API
		- Для создания индекса через Fluent API применяется метод HasIndex():
			modelBuilder.Entity<User>().HasIndex(u => u.Passport);
		- С помощью дополнительного метода IsUnique() можно указать, что индекс должен иметь уникальное значение
			modelBuilder.Entity<User>().HasIndex(u => u.Passport).IsUnique();
		- Составные индексы
			modelBuilder.Entity<User>().HasIndex(u => new { u.Passport, u.PhoneNumber });
		- Имя индекса
			modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).HasDatabaseName("PhoneIndex");
		- Фильтры индексов
			Некоторые системы управления базами данных позволяют определять индексы с фильрами или частичные индексы, которые позволяют выполнять индексацию 
			только по ограниченному набору значений, что увеличивает производительность и уменьшает использование дискового простанства. И EntityFramework Core 
			также позволяет создавать подобные индексы. Для этого применяется метод HasFilter(), в который передается sql-выражение, которое определяет 
			условие фильтра. Например:
				modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).HasFilter("[PhoneNumber] IS NOT NULL");

-- Генерация значений свойств и столбцов
	-- Генерация ключей
		По умолчанию для свойств первичных ключей, которые представляют типы int или GUID и которые имеют значение по умолчанию, генерируется значение при вставке в базу данных. 
		Для всех остальных свойств значения по умолчанию не генерируется.
		-- Атрибут DatabaseGeneratedAttribute
			- отключить автогенерацию значения при добавлении:
			  [DatabaseGenerated(DatabaseGeneratedOption.None)]
			   public int Id { get; set; }
			- Если мы хотим, чтобы база данных, наоборот, сама генерировала значение, то в атрибут надо передавать значение DatabaseGeneratedOption.Identity:
			   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
				public int Id { get; set; }				// Но в данном случае для свойства Id это значение избыточно, так как значение генерируется по умолчанию.
		-- Fluent API
			- Отключение автогенерации значения для свойства с помощью Fluent API:
				modelBuilder.Entity<User>().Property(b => b.Id).ValueGeneratedNever();
	-- Значения по умолчанию
		Для свойств, которые не представляют ключи и для которых не устанавливается значения, используются значения по умолчанию. Например, для свойств типа int это значение 0. 
		С помощью метода HasDefaultValue() можно переопределить значение по умолчанию, которое будет применяться после добавления объекта в базу данных:
			- HasDefaultValue()
				modelBuilder.Entity<User>().Property(u => u.Age).HasDefaultValue(18);
			- HasDefaultValueSql()
				Метод HasDefaultValueSql() также определяет генерацию значения по умолчанию, только само значение устанавливается на основе кода SQL, который передается в этот метод
				 modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("DATETIME('now')");
					Поскольку в данном случае используется база данных SQLite, то в качестве SQL-выражения передается вызов функции DATETIME('now') - "now" здесь указывает, 
					что мы хотим получить текущую дату.
	-- Вычисляемые столбцы
		- HasComputedColumnSql()
			modelBuilder.Entity<User>().Property(u => u.Name).HasComputedColumnSql("FirstName || ' ' || LastName");

-- Ограничения свойств
	-- Установка ограничений С помощью метода HasCheckConstraint() можно установить ограничение для столбца. На уровне базы данных это приведет к установке для столбца атрибута CHECK, 
	   который задает ограничение.
			modelBuilder.Entity<User>().HasCheckConstraint("Age", "Age > 0 AND Age < 120"); 
		- В качестве третьего параметра в HasCheckConstraint, передается делегат Action, который принимает объект CheckConstraintBuilder для настройки ограничия.
			modelBuilder.Entity<User>().HasCheckConstraint("Age", "Age > 0 AND Age < 120", c => c.HasName("CK_User_Age"));

	-- Ограничения по длине (Ограничение максимальной длины применяется только к строкам и к массивам, например, byte[].)
		- Атрибут MaxLength
			[MaxLength(50)]
			public string? Name { get; set; }
				Стоит отметить, что данное ограничение будет действовать только для тех систем баз данных, которые поддерживают данную возможность. Например, для бд SQLite это 
				не будет иметь никакого значения. А в случае с бд MS SQL Server столбец Name в базе данных будет иметь тип nvarchar(50) и тем самым иметь ограничение по длине.
		- Метод HasMaxLength (Fluent API)
			modelBuilder.Entity<User>().Property(b => b.Name).HasMaxLength(50);
