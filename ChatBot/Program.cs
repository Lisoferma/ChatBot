using System.Text.RegularExpressions;
using static LisofermaChatBot.UserCommands;

namespace LisofermaChatBot;

internal class Program
{
    static void Main()
    {
        ChatBotController chatbot = new();
        string? input;

        chatbot.AddInputHandler(
            new Regex(@"(time)|(время)|(который час)|(который час)"),
            GetTime);

        chatbot.AddInputHandler(
            new Regex(@"(date)|(дата)|(какая сегодня дата)"),
            GetDate);

        chatbot.AddInputHandler(
            new Regex(@"(calculate)|(compute)|(вычисли)|(посчитай)"),
            CalculateExpressionFromString);

        chatbot.AddInputHandler(
            new Regex(@"(currency rate)|(курс валют)|(курс)"),
            GetCurrencyRate);

        do
        {
            Console.Write("You: ");
            input = Console.ReadLine();

            string answer = chatbot.GetAnswer(input!);
            Console.WriteLine("Bot: " + answer);
        }
        while (input != "q");
    }
}
