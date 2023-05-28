namespace LisofermaChatBot;

internal class Program
{
    static void Main()
    {
        IBotResponse chatbot = new ChatBotAIML();
        string? input;

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
