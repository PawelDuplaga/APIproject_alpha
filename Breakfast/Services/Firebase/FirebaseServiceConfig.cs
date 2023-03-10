using System.Xml.Serialization;

namespace Breakfast.Services.Firebase;

[XmlRoot("FirebaseServiceConfig")]
public class FirebaseServiceConfig
{
    [XmlElement("AuthSecret")]
    public string AuthSecret { get; set; }

    [XmlElement("BasePath")]
    public string BasePath { get; set; }
}
