using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ChatBot;

/// <summary>
/// Предоставляет методы для сохранения <see cref="ObservableCollection<Message>"/>
/// в формате XML.
/// </summary>
public class ChatHistoryStorageXml : IChatHistoryStorage
{
    // Путь к файлу сохранения.
    private string _filepath;

    /// <summary>
    /// Путь к файлу сохранения.
    /// </summary>
    public string Filepath
    {
        get => _filepath;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(
                    nameof(Filepath), "Filepath cannot be null or empty");
            }

            _filepath = value;
        }
    }


    /// <summary>
    /// Инициализирует и задаёт <see cref="Filepath"/> как "ChatHistory.xml".
    /// </summary>
    public ChatHistoryStorageXml() : this("ChatHistory.xml") { }


    /// <summary>
    /// Инициализирует и задаёт <see cref="Filepath"/> заданным значением.
    /// </summary>
    /// <param name="filepath">Путь к файлу сохранения</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ChatHistoryStorageXml(string filepath)
    {
        if (string.IsNullOrEmpty(filepath))
        {
            throw new ArgumentNullException(
                nameof(_filepath), "Filepath cannot be null or empty");
        }

        _filepath = filepath;
    }


    /// <summary>
    /// Сохраненить <see cref="ObservableCollection<Message>"/>
    /// в XML файл; путь к файлу - <see cref="Filepath"/>.
    /// </summary>
    /// <param name="chatHistory">История чата которую нужно сохранить.</param>
    public void Save(ObservableCollection<Message> chatHistory)
    {
        // xml объекта chatHistory
        string xml =
            ObjectSerializerToXml<ObservableCollection<Message>>.ToXml(chatHistory);

        using StreamWriter writer = new(Filepath, false);
        writer.WriteLine(xml);
    }


    /// <summary>
    /// Сохраненить <see cref="ObservableCollection<Message>"/>. 
    /// </summary>
    /// <param name="chatHistory">История чата которую нужно сохранить.</param>
    /// <param name="filepath">Путь к файлу сохранения.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SaveAs(ObservableCollection<Message> chatHistory, string filepath)
    {
        if (string.IsNullOrEmpty(filepath))
        {
            throw new ArgumentNullException(
                nameof(_filepath), "Filepath cannot be null or empty");
        }

        // xml объекта chatHistory
        string xml =
            ObjectSerializerToXml<ObservableCollection<Message>>.ToXml(chatHistory);

        using StreamWriter writer = new(filepath, false);
        writer.WriteLine(xml);
    }


    public ObservableCollection<Message>? LoadFrom(string filepath)
    {
        //throw new NotImplementedException();

        if (string.IsNullOrEmpty(filepath))
        {
            throw new ArgumentNullException(
                nameof(_filepath), "Filepath cannot be null or empty");
        }

        string xmlFromFile;

        try
        {
            using StreamReader reader = new(filepath);
            xmlFromFile = reader.ReadToEnd();
        }
        catch
        {
            return null;
        }

        if (string.IsNullOrEmpty(xmlFromFile))
            return null;

        return ObjectSerializerToXml<ObservableCollection<Message>>.FromXml(xmlFromFile);   
    }
}
