namespace Sorux.Bot.Core.Kernel.Utils
{
    public interface ILoggerService
    {
        void Info(string source, string msg);
        void Warn(string source, string msg);
        void Error(string source, string msg);
        void Fatal(string source, string msg);
        void Debug(string source, string msg);

        void Info<T>(string source, string msg, T Context);

        void Warn<T>(string source, string msg, T Context);

        void Error<T>(string source, string msg, T Context);
        void Fatal<T>(string source, string msg, T Context);
        void Debug<T>(string source, string msg, T Context);

        void Error<T>(Exception exception, string source, string msg, T Context);
        void Fatal<T>(Exception exception, string source, string msg, T Context);
    }
}