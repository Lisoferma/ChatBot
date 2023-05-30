namespace ChatBot;

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
        get => Text;

        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(Author), "Value cannot be null or empty");

            Text = value;
        }      
    }


    /// <summary>
    /// Инициализирует сообщение пустым текстом и Undefined автором.
    /// </summary>
    public Message()
    {
        Text = string.Empty;
        Author = "Undefined";
    }
}
