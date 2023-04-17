using AIMLbot;

namespace LisofermaChatBot;

public class ChatBot : IBotResponse
{
    const string Username = "username";
    private Bot AimlBot;
    private User myUser;


    public ChatBot()
    {
        AimlBot = new Bot();
        myUser = new User(Username, AimlBot);
        Initialize();
    }


    private void Initialize()
    {
        AimlBot.loadSettings();
        AimlBot.isAcceptingUserInput = false;
        AimlBot.loadAIMLFromFiles();
        AimlBot.isAcceptingUserInput = true;
    }


    public string GetAnswer(string input)
    {
        Request request = new Request(input, myUser, AimlBot);
        Result result = AimlBot.Chat(request);
        return result.Output;
    }
}

