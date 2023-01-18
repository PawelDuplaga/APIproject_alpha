using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using Breakfast.Utils;

namespace Breakfast.ApiLogger;

public class ApiLogger : ILogger
{
    private readonly string _name;  
    private readonly ApiLoggerConfig _config;
    private readonly string _className;

    public ApiLogger(string name, ApiLoggerConfig config)
    {
        _config = config;
        _name = name;

        StackTrace stackTrace = new StackTrace();
        _className = stackTrace.GetFrame(1).GetMethod().ReflectedType.Name;
    }


    public void LogDebug(string message, int eventId = 0, Exception exception = null, Func<string, Exception, string> formatter = null)
    {
        Log(LogLevel.Debug, new EventId(eventId, ""), message, exception, formatter);
    }

    public void LogInformation(string message, int eventId = 0, Exception exception = null, Func<string, Exception, string> formatter = null)
    {
        Log(LogLevel.Information, new EventId(eventId, ""), message, exception, formatter);
    }

    public void LogWarning(string message, int eventId = 0, Exception exception = null, Func<string, Exception, string> formatter = null)
    {
        Log(LogLevel.Warning, new EventId(eventId, ""), message, exception, formatter);
    }

    public void LogError(string message, int eventId = 0, Exception exception = null, Func<string, Exception, string> formatter = null)
    {
        Log(LogLevel.Error, new EventId(eventId, ""), message, exception, formatter);
    }



    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == _config.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception = null, Func<TState, Exception, string> formatter = null)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        if (_config.EventId == 0 || _config.EventId == eventId.Id)
        {
            string message;
            if(formatter != null)
            {
                message = formatter(state, exception);
            }
            else
            {
                message = state.ToString();
            }
            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                WriteMessage(logLevel, _name, eventId.Id, message, exception);
            }
        }
    }

    private void WriteMessage(LogLevel logLevel, string name, int eventId, string message, Exception exception)
    {
        string logMessage = $"{DateTime.Now} [{logLevel}] {name} {eventId}: {message}";

        if (exception != null)
        {
            logMessage += Environment.NewLine + exception.ToString();
        }

        File.AppendAllText(Path.Combine(_config.LogFolderPath, _className), logMessage + Environment.NewLine);
        Console.WriteLine(logMessage);
    }

}