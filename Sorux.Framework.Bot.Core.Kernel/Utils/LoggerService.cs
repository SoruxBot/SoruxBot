﻿using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace Sorux.Framework.Bot.Core.Kernel.Utils
{
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<LoggerService> _logger;
        private readonly ConcurrentDictionary<Type, ILogger> _map = new ConcurrentDictionary<Type, ILogger>();
        private static string LoggerPath = Directory.GetCurrentDirectory();

        private string GetCurrentLogFile()
            => LoggerPath + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

        private static void CreateDirIfNotExists(string path)
        {
            path = Path.Join(Directory.GetCurrentDirectory(), path);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        public LoggerService(ILoggerFactory loggerFactory, ILogger<LoggerService> logger)
        {
            this._loggerFactory = loggerFactory;
            this._logger = logger;
            CreateDirIfNotExists("Logs");
            LoggerPath = Path.Join(LoggerPath, "Logs");
        }

        public ILogger GetLoggerObj()
        {
            StackTrace trace = new StackTrace();
            StackFrame? frame = trace.GetFrame(3);
            if (frame == null)
            {
                return _logger;
            }

            MethodBase? logMethod = frame.GetMethod();
            if (logMethod == null)
            {
                return _logger;
            }

            Type? logType = logMethod.ReflectedType;
            if (logType == null)  return _logger;

            return _map.GetOrAdd(logType, sp => _loggerFactory.CreateLogger(sp));
        }

        public void Debug(string source, string msg)
        {
            GetLoggerObj().LogDebug($"[{DateTime.Now:HH:mm:ss}][{source}] {msg}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} \n");
        }

        public void Debug<T>(string source, string msg, T Context)
        {
            GetLoggerObj().LogDebug($"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context} \n");
        }

        public void Error(string source, string msg)
        {
            GetLoggerObj().LogError($"[{DateTime.Now:HH:mm:ss}][{source}] {msg}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} \n");
        }

        public void Error<T>(string source, string msg, T Context)
        {
            GetLoggerObj().LogError($"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context} \n");
        }

        public void Error<T>(Exception exception, string source, string msg, T Context)
        {
            GetLoggerObj().LogError(exception, $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context} \n");
        }

        public void Fatal(string source, string msg)
        {
            GetLoggerObj().LogCritical($"[{DateTime.Now:HH:mm:ss}][{source}] {msg}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} \n");
        }

        public void Fatal<T>(string source, string msg, T Context)
        {
            GetLoggerObj().LogCritical($"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context} \n");
        }

        public void Fatal<T>(Exception exception, string source, string msg, T Context)
        {
            GetLoggerObj().LogCritical(exception, $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}\n");
        }

        public void Info(string source, string msg)
        {
            GetLoggerObj().LogInformation($"[{DateTime.Now:HH:mm:ss}][{source}] {msg}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg}\n");
        }

        public void Info<T>(string source, string msg, T Context)
        {
            GetLoggerObj().LogInformation($"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}\n");
        }

        public void Warn(string source, string msg)
        {
            GetLoggerObj().LogWarning($"[{DateTime.Now:HH:mm:ss}][{source}] {msg}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg}\n");
        }

        public void Warn<T>(string source, string msg, T Context)
        {
            GetLoggerObj().LogWarning($"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}");
            File.AppendAllText(GetCurrentLogFile(), $"[{DateTime.Now:HH:mm:ss}][{source}] {msg} {Context}\n");
        }
    }
}