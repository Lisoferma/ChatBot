using System.Text.RegularExpressions;

namespace LisofermaChatBot;

/// <summary>
/// Контроллер для обработки сообщений пользователя.
/// Содержит список команд для пользователя и текущий чатбот.
/// </summary>
public class ChatBotController : IBotResponse
{
    /// <summary>
    /// Текущий чатбот от которого пользователь будет получать ответ.
    /// </summary>
    public IBotResponse CurrentBot { get; set; }

    /// <summary>
    /// Делегат для методов обработки сообщений и команд пользователя.
    /// </summary>
    /// <param name="input">Сообщение от пользователя.</param>
    /// <returns>Ответ на сообщение.</returns>
    public delegate string InputHandler(string input);

    // Обработчики сообщений и regex который их вызывает.
    private Dictionary<Regex, InputHandler> _dictInputHandlers;


    /// <summary>
    /// Инициализирует текущий чатбот как ChatBotAIML.
    /// </summary>
    public ChatBotController() : this(new ChatBotAIML()) { }


    /// <summary>
    /// Инициализировать и задать текущий чатбот. 
    /// </summary>
    /// <param name="chatBot">Чатбот который будет отвечать на сообщения.</param>
    public ChatBotController(IBotResponse chatBot)
    {       
        CurrentBot = chatBot;
        _dictInputHandlers = new Dictionary<Regex, InputHandler>();
    }


    /// <summary>
    /// Получить ответ на сообщение от подходящего обработчика
    /// или, если его нет, получить ответ от чатбота.
    /// </summary>
    /// <param name="input">Сообщение от пользователя.</param>
    /// <returns>Ответ на сообщение.</returns>
    public string GetAnswer(string input)
    {
        // Поиск подходящего обработчика ввода, если не найден - null.
        InputHandler? foundHandler = 
            _dictInputHandlers.FirstOrDefault(handler => handler.Key.IsMatch(input)).Value;

        if (foundHandler != null)
            return foundHandler(input);
        else
            return CurrentBot.GetAnswer(input);
    }


    /// <summary>
    /// Добавить обработчик сообщения пользователя.
    /// </summary>
    /// <param name="regex">Regex который будет вызывать обработчик.</param>
    /// <param name="inputHandler">Обработчик сообщения.</param>
    public void AddInputHandler(Regex regex, InputHandler inputHandler)
    {
        _dictInputHandlers.Add(regex, inputHandler);
    }
}

