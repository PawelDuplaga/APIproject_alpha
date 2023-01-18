namespace Breakfast.ApiLogger;

public class ApiLoggerProvider : ILoggerProvider
{
    private readonly ApiLoggerConfiguration _config;

    public ApiLoggerProvider(ApiLoggerConfiguration config)
    {
        _config = config;
    }
    public ILogger CreateLogger(string name)
    {
        return new ApiLogger(name, _config);
    }

    public void Dispose()
    {
        // Dispose of any resources that were created
    }
}