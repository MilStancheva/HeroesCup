using System;

namespace HeroesCup.Web.Common.Middlewares.Exceptions
{
    public class UnhandledExceptionLogEntry
    {
        public DateTime DateGenerated { get; }
        public string Name { get; }
        public string Message { get; }
        public string StackTrace { get; }

        private UnhandledExceptionLogEntry(DateTime dateGenerated, string name,
            string message, string stackTrace)
        {
            this.DateGenerated = dateGenerated;
            this.Name = name;
            this.Message = message;
            this.StackTrace = stackTrace;
        }

        public override string ToString()
        {
            var type = $"Type: {this.Name}";
            var datePart = $"Date: {this.DateGenerated:dd/MM/yyyy}";
            var messagePart = $"Message: {this.Message}";
            var stackTracePart = $"Stack Trace: {this.StackTrace}";

            return string.Format("{1}{0}{1}{2}{1}{3}{1}{4}{1}",
                type, Environment.NewLine, datePart, messagePart, stackTracePart);
        }

        public static UnhandledExceptionLogEntry FromException(Exception exception) =>
            new UnhandledExceptionLogEntry(DateTime.UtcNow, exception.GetType().Name,
                exception.Message, exception.StackTrace);
    }
}