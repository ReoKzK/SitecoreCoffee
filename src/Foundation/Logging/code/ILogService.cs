using System;

namespace SitecoreCoffee.Foundation.Logging
{
    public interface ILogService
    {
        ILogService Initialize(object callerInstance);

        void Debug(string message);
        void Debug(string message, params object[] args);
        void Debug(string message, Type callingType);
        void Info(string message);
        void Info(string message, params object[] args);
        void Info(string message, Type callingType);
        void Warn(string message);
        void Warn(string message, params object[] args);
        void Warn(string message, Type callingType);
        void Error(string message);
        void Error(string message, params object[] args);
        void Error(string message, Type callingType);
        void Error(Exception exception);
        void Error(Exception e, Type callingType);
        void Fatal(string message);
        void Fatal(string message, params object[] args);
        void Fatal(string message, Type callingType);
        void Fatal(Exception e);
        void Fatal(Exception e, Type callingType);
        void OpenProfile(string message);
        void EndProfile(string message);
    }
}
