using Breakfast.Utils;

namespace Breakfast.ApiLogger;


public class ApiLoggerProvider : ILoggerProvider
{
    private readonly ApiLoggerConfig _config;

    public ApiLoggerProvider()
    {
        _config = XmlConfigReader<ApiLoggerConfig>.GetConfig(ApiBreakfastConstants.APILOGGER_CONFIG_FILE_PATH);
    }
    public ApiLoggerProvider(ApiLoggerConfig config)
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