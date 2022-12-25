
-- Параллелизм
В ситуации, когда множество пользователей одновременно имеют доступ к одинаковому набору данных и могут эти данные изменять, мы можем столкнуться с проблемой параллелизма.
Для решения проблемы параллелизма Entity Framework Core предлагает использовать токены параллелизма или concurrency token.
Если свойство установлено в качестве concurrency token (токен параллелизма), Entity Framework перед сохранением изменений будет проверять его значение. Если значение отличается, 
то какой-то другой пользователь уже произвел изменения над данными. В этом случае EF Core применяет принцип оптимистичного параллелизма (optimistic concurrency), 
при котором, если сохраняемые данные уже были кем-то изменены, то выбрасывается ошибка.

	-- Токены параллелизма или concurrency token
		-- Атрибут ConcurrencyCheck
			Атрибут ConcurrencyCheck позволяет решить проблему параллелизма, когда с одной и той же записью в таблице могут работать одновременно несколько пользователей. 

		-- Метод IsConcurrencyToken
			В Fluent API токен параллелизма настраивается с помощью метода IsConcurrencyToken():
			- modelBuilder.Entity<User>().Property(b => b.Name).IsConcurrencyToken();
				[ConcurrencyCheck]
			    public string? Name { get; set; }

	-- Атрибут Timestamp (!!! Зависит от провайдера в SQLite не работае, в MS SQL работает)
		Другой механизм по отслеживанию изменений объекта в БД представляет атрибут Timestamp.
		И если два пользователя одновременно начнут редактировать одну и ту же строку, то после сохранения модели первым пользователем, второй пользователь 
		получит исключение DbUpdateConcurrencyException

		-- Fluent API и метод IsRowVersion ()(!!! Зависит от провайдера в SQLite не работае, в MS SQL работает)
			public class User
			{
				public int Id { get; set; }
				public string? Name { get; set; }
				public byte[]? Timestamp { get; set; }
			}
			public class ApplicationContext : DbContext
			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.Entity<User>().Property(b => b.Timestamp).IsRowVersion();
			}


