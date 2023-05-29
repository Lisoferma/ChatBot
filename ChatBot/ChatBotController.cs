using System.Text.RegularExpressions;

namespace LisofermaChatBot;

public class ChatBotController : IBotResponse
{
    public IBotResponse CurrentBot { get; set; }

    public delegate string InputHandler(string input);

    private Dictionary<Regex, InputHandler> _dictInputHandlers;


    public ChatBotController() : this(new ChatBotAIML()) { }


    public ChatBotController(IBotResponse chatBot)
    {       
        CurrentBot = chatBot;
        _dictInputHandlers = new Dictionary<Regex, InputHandler>();
    }


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


    public void AddInputHandler(Regex regex, InputHandler inputHandler)
    {
        _dictInputHandlers.Add(regex, inputHandler);
    }
}

