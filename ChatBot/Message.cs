namespace ChatBot;

/// <summary>
/// Сообщение, содержит текст и его автора.
/// </summary>
public class Message
{
    // Кем написано сообщение.
    private string _author;

    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Кем написано сообщение.
    /// </summary>
    public string Author
    {
        get => _author;

        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(Author), "Value cannot be null or empty");

            _author = value;
        }      
    }

    /// <summary>
    /// Время отправки.
    /// </summary>
    public DateTime TimeStamp { get; set; }


    /// <summary>
    /// Инициализирует сообщение пустым текстом, Undefined автором
    /// и текущим временем.
    /// </summary>
    public Message() : this(string.Empty, "Undefined", DateTime.Now) { }


    /// <summary>
    /// Инициализирует сообщение заданными свойствами
    /// и текущим временем.
    /// </summary>
    /// <param name="text">Текст сообщения.</param>
    /// <param name="author">От кого сообщение.</param>
    public Message(string text, string author) : this(text, author, DateTime.Now) { }


    /// <summary>
    /// Инициализирует сообщение заданными свойствами.
    /// </summary>
    /// <param name="text">Текст сообщения.</param>
    /// <param name="author">От кого сообщение.</param>
    /// <param name="timeStamp">Время отправки.</param>
    public Message(string text, string author, DateTime timeStamp)
    {
        if (string.IsNullOrEmpty(author))
            throw new ArgumentNullException(nameof(author), "Author cannot be null or empty");

        Text = text;
        _author = author;
        TimeStamp = timeStamp;
    }
}
