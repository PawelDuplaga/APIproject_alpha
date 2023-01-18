using System.Xml.Serialization;

namespace Breakfast.ApiLogger;

[XmlRoot("ApiLoggerConfig")]
public class ApiLoggerConfig
{
    [XmlElement("LogLevel")]
    public LogLevel LogLevel { get; set;}
    
    [XmlElement("EventId")]
    public EventIdConfig EventId { get; set;}

    [XmlElement("LogFolerPath")]
    public string LogFolderPath { get; set;}
}

[XmlRoot("EventId")]
public class EventIdConfig
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }
}