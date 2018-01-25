namespace SitecoreCoffee.Foundation.Logging
{
    public interface ILoggerService
    {
        void Debug(string message);

        void Error(string message);

        void SingleError(string message);

        void Info(string message);

        void Warn(string message);

        void Fatal(string message);
    }
}
