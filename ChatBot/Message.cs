namespace ChatBot;

/// <summary>
/// Сообщение, содержит текст и его автора.
/// </summary>
public class Message
{
    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Кем написано сообщение.
    /// </summary>
    public string Author
    {
        get => Author;

        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(Author), "Value cannot be null or empty");

            Author = value;
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
    public Message()
    {
        Text = string.Empty;
        Author = "Undefined";
        TimeStamp = DateTime.Now;
    }


    /// <summary>
    /// Инициализирует сообщение заданными свойствами.
    /// </summary>
    /// <param name="text">Текст сообщения.</param>
    /// <param name="author">От кого сообщение.</param>
    /// <param name="timeStamp">Время отправки.</param>
    public Message(string text, string author, DateTime timeStamp)
    {
        Text = text;
        Author = author;
        TimeStamp = timeStamp;
    }


    /// <summary>
    /// Инициализирует сообщение заданными свойствами
    /// и текущим временем.
    /// </summary>
    /// <param name="text">Текст сообщения.</param>
    /// <param name="author">От кого сообщение.</param>
    public Message(string text, string author)
    {
        Text = text;
        Author = author;
        TimeStamp = DateTime.Now;
    }
}
