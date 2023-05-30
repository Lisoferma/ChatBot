using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using static LisofermaChatBot.UserCommands;
using ChatBot;

namespace LisofermaChatBot;

internal class Program
{
    static void Main()
    {
        ObservableCollection<Message> messages = new();
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

            Message userMessage = new()
            {
                Text = input ?? string.Empty,
                Author = "User",
                TimeStamp = DateTime.Now
            };

            Console.WriteLine(userMessage.Author);
            Console.WriteLine(userMessage.Text);
            Console.WriteLine(userMessage.TimeStamp);

            messages.Add(userMessage);

            string answer = chatbot.GetAnswer(input!);
            Console.WriteLine("Bot: " + answer);

            Message botMessage = new()
            {
                Text = answer ?? string.Empty,
                Author = "Bot",
                TimeStamp = DateTime.Now
            };

            messages.Add(botMessage);
        }
        while (input != "q");

        foreach (Message item in messages)
        {
            Console.WriteLine(item.Author);
            Console.WriteLine(item.Text);
            Console.WriteLine(item.TimeStamp);
        }

        string path = "messages.xml";
        string xml =
            ObjectSerializerToXml<ObservableCollection<Message>>.ToXml(messages);

        using (StreamWriter writer = new(path, false))
        {
            writer.WriteLine(xml);
        }
    }
}
