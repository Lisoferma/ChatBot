using AIMLbot;

namespace ChatBot;

/// <summary>
/// Чатбот основанный на технологии AIML.
/// </summary>
public class ChatBotAIML : IBotResponse
{   
    // AIML чатбот.
    private readonly Bot _botAIML;

    // Используется для инициализации бота.
    private readonly User _user;

    // Используется для инициализации бота.
    public const string Username = "username";


    public ChatBotAIML()
    {
        _botAIML = new Bot();
        _user = new User(Username, _botAIML);
        Initialize();
    }


    /// <summary>
    /// Загружает конфигурацию бота и AIML файл,
    /// путь которого указан в конфигурации.
    /// </summary>
    private void Initialize()
    {
        _botAIML.loadSettings();
        _botAIML.loadAIMLFromFiles();
    }


    /// <summary>
    /// Получить ответ от бота, на основе строки.
    /// </summary>
    /// <param name="input">Строка которую будет обрабатывать бот.</param>
    /// <returns>Ответ от бота.</returns>
    public string GetAnswer(string input)
    {
        Request request = new (input, _user, _botAIML);
        Result result = _botAIML.Chat(request);
        return result.Output;
    }
}

