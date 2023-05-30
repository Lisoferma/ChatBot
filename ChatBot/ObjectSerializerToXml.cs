using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace ChatBot;

/// <summary>
/// Сериализатор и десереализатор объектов в XML.
/// </summary>
/// <typeparam name="T">Тип сериализуемого объекта.</typeparam>
public static class ObjectSerializerToXml<T>
{ 
    /// <summary>
    /// Сериализовать объект в XML.
    /// </summary>
    /// <param name="value">Объект который нужно сериализовать.</param>
    /// <returns>XML сериализованного объекта.</returns>
    public static string ToXml(T value)
    {
        XmlSerializer serializer = new(typeof(T));
        StringBuilder stringBuilder = new();
        XmlWriterSettings settings = new()
        {
            Indent = true,
            OmitXmlDeclaration = true,
        };

        using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
        {
            serializer.Serialize(xmlWriter, value);
        }

        return stringBuilder.ToString();
    }


    /// <summary>
    /// Десереализовать объект из xml.
    /// </summary>
    /// <param name="xml">XML из которого нужно десереализовать объект.</param>
    /// <returns>Десереализваонный объект.</returns>
    public static T? FromXml(string xml)
    {
        XmlSerializer serializer = new(typeof(T));
        T? value;

        using (StringReader stringReader = new(xml))
        {
            object? deserialized = serializer.Deserialize(stringReader);
            value = (T?)deserialized;
        }

        return value;
    }
}
