using System.Text.RegularExpressions;

namespace LisofermaChatBot;

public class ChatBotController : IBotResponse
{
    public delegate string InputHandler(string input);
    private Dictionary<Regex, InputHandler> _dictInputHandlers;

    private IBotResponse CurrentBot;


    public ChatBotController()
    {
        CurrentBot = new ChatBotAIML();
    }


    public ChatBotController(IBotResponse chatBot)
    {
        CurrentBot = chatBot;
    }


    public string GetAnswer(string input)
    {
        string? answer = "";

        answer = _listOfInputHandlers?.Invoke(input);
        if (!String.IsNullOrEmpty(answer))
            return answer;

        return CurrentBot.GetAnswer(input);
    }


    public void AddInputHandler(Regex regex, InputHandler inputHandler)
    {
        _dictInputHandlers.Add(regex, inputHandler);
    }
}

