
Провайдеры логгирования

Провайдеры логгирования собственно представляют те объекты, которые отвечают за логику логгирования. И в данном случае мы можем либо воспользоваться встроенными провайдерами, 
либо создать свой.

-- Создание своего провайдера логгирования
	Вначале рассмотрим, как создать свой провайдер логгирования. Это может быть полезно, если нас не устраивают встроенные средства логгирования, например, метод LogTo(), 
	и мы хотим по своему настроить логику логгирования.
	-- 1. Создаем свой класс MyLoggerProvider(напр.), реализующий интерфейс ILoggerProvider(using Microsoft.Extensions.Logging)
		В этом интерфейсе определены два метода:
			- CreateLogger: создает и возвращает объект логгера. Для создания логгера используется путь к файлу, который передается через конструктор
			- Dispose: управляет освобождение ресурсов. В данном случае пустая реализация
				CreateLogger возвращает экз класса реализующий интерфейс ILogger
					- Создаю класс MyLogger реализующий интерфейс ILogger
						ILogger определяет 3 метода
							- 1. BeginScope
							- 2. IsEnabled - возвращает значения true или false, которые указывает, доступен ли логгер для использования.
							- 3. Log: этот метод предназначен для выполнения логгирования. Он принимает пять параметров:
								- LogLevel: уровень детализации текущего сообщения
								- EventId: идентификатор события
								- TState: некоторый объект состояния, который хранит сообщение
								- Exception: информация об исключении
								- formatter: функция форматирования, которая с помощью двух предыдущих параметров позволяет получить собственно сообщение для логгирования

-- Способы применения логгирование для контекста данных(вышеуказанных логеров)
	-- Локальное применение провайдера
		using Microsoft.Extensions.Logging;
		using (ApplicationContext db = new ApplicationContext())
		{
			db.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
			....

	-- Глобальная установка логгирования в контексте данных
		- в public class ApplicationContext : DbContext
			    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
				{
					builder.AddProvider(new MyLoggerProvider());    // указываем наш провайдер логгирования
				});
				пределяем статическую переменную MyLoggerFactory, которой присваивается результат метода LoggerFactory.Create
		- Затем в методе OnConfiguring устанавливаем данную фабрику логгера:
			optionsBuilder.UseLoggerFactory(MyLoggerFactory);