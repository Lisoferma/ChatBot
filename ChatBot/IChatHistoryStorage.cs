using System.Collections.ObjectModel;

namespace ChatBot;

/// <summary>
/// Предоставляет методы для сохранения и загрузки <see cref="ObservableCollection<Message>"/>.
/// </summary>
public interface IChatHistoryStorage
{
    /// <summary>
    /// Путь к файлу сохранения.
    /// </summary>
    public string Filepath { get; set; }


    /// <summary>
    /// Сохраненить <see cref="ObservableCollection<Message>"/>;
    /// путь к файлу - <see cref="Filepath"/>.
    /// </summary>
    /// <param name="chatHistory">История чата которую нужно сохранить.</param>
    void Save(ObservableCollection<Message> chatHistory);


    /// <summary>
    /// Сохраненить <see cref="ObservableCollection<Message>"/>. 
    /// </summary>
    /// <param name="chatHistory">История чата которую нужно сохранить.</param>
    /// <param name="filepath">Путь к файлу сохранения.</param>
    /// <exception cref="ArgumentNullException"></exception>
    void SaveAs(ObservableCollection<Message> chatHistory, string filepath);


    /// <summary>
    /// Загрузить историю чата из файла.
    /// </summary>
    /// <param name="filepath">Файл из которого нужно загрузить.</param>
    /// <returns>История чата.</returns>
    ObservableCollection<Message>? LoadFrom(string filepath);
}
