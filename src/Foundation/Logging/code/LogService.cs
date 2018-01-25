namespace SitecoreCoffee.Foundation.Logging
{
    public class LogService : ILoggerService
    {
        //TODO: Fit into a pattern wrapping the API calls outside of the service
        public void Debug(string message)
        {
            Sitecore.Diagnostics.Log.Debug(message, this);
        }

        public void Error(string message)
        {
            Sitecore.Diagnostics.Log.Error(message, this);
        }

        public void SingleError(string message)
        {
            Sitecore.Diagnostics.Log.SingleError(message, this);
        }

        public void Info(string message)
        {
            Sitecore.Diagnostics.Log.Info(message, this);
        }

        public void Warn(string message)
        {
            Sitecore.Diagnostics.Log.Warn(message, this);
        }

        public void Fatal(string message)
        {
            Sitecore.Diagnostics.Log.Fatal(message, this);
        }
    }
}