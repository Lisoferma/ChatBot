using AIMLbot;

namespace ChatBot;

public class ChatBotAIML : IBotResponse
{
    public const string Username = "username";
    private readonly Bot _botAIML;
    private readonly User _user;


    public ChatBotAIML()
    {
        _botAIML = new Bot();
        _user = new User(Username, _botAIML);
        Initialize();
    }


    private void Initialize()
    {
        _botAIML.loadSettings();
        _botAIML.loadAIMLFromFiles();
    }


    public string GetAnswer(string input)
    {
        Request request = new (input, _user, _botAIML);
        Result result = _botAIML.Chat(request);
        return result.Output;
    }
}

