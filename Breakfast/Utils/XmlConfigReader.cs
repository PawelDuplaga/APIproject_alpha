using System;
using System.Xml.Serialization;
using System.IO;

namespace Breakfast.Utils;

public static class XmlConfigReader<T> where T : new()
{
    public static T GetConfig(string filePath)
    {
        T config;
        XmlSerializer serializer = new XmlSerializer(typeof(T));

          using (FileStream stream = new FileStream(filePath, FileMode.Open))
        {
            config = (T)serializer.Deserialize(stream);
        }

        return config;
    }

}