using System.Xml.Serialization;

namespace Breakfast.ApiLogger;

[XmlRoot("ApiLoggerConfig")]
public class ApiLoggerConfig
{
    [XmlElement("LogLevel")]
    public LogLevel LogLevel { get; set;}
    
    [XmlElement("EventId")]
    public EventId EventId { get; set;}

    [XmlElement("LogFolerPath")]
    public string LogFolderPath { get; set;}
}