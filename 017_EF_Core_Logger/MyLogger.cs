using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace _017_EF_Core_Logger
{
    internal class MyLogger : ILogger, IDisposable
    {
        private readonly string filePath;
        public MyLogger()
        {
            filePath= @"D:\0_valexproject\00_Metanit\EF_Core_2022\017_EF_Core_Logger\log.txt";
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        public void Dispose() {}

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            File.AppendAllText(filePath, formatter(state, exception));
            Console.WriteLine(formatter(state,exception));
        }
    }
}
