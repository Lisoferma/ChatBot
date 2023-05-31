using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using ChatBot;
using static ChatBot.UserCommands;

namespace ChatBotGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public string Username { get; set; }
    public const string BotName = "Bot";

    public ObservableCollection<Message> chatHistory;
    private readonly ChatBotController chatbot;


    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        Username = "User";
        chatHistory = new ObservableCollection<Message>();
        chatbot = new ChatBotController();

        Message userMessage = new()
        {
            Text = "test1231231232342 432452",
            Author = Username,
            TimeStamp = DateTime.Now
        };
        chatHistory.Add(userMessage);
        
        ListView_chat.ItemsSource = chatHistory;

        InitializeInputHandler(chatbot);
    }


    private void Button_send_Click(object sender, RoutedEventArgs e)
    {
        GetChatbotAnswer();
    }


    private void GetChatbotAnswer()
    {
        string input = TextBox_input.Text;
        TextBox_input.Text = string.Empty;

        if (string.IsNullOrEmpty(input)) return;

        Message userMessage = new()
        {
            Text = input,
            Author = Username,
            TimeStamp = DateTime.Now
        };

        chatHistory.Add(userMessage);

        string answer = chatbot.GetAnswer(input);

        Message botMessage = new()
        {
            Text = answer,
            Author = BotName,
            TimeStamp = DateTime.Now
        };

        chatHistory.Add(botMessage);

        ListView_chat.ScrollIntoView(chatHistory.LastOrDefault());
    }


    private void InitializeInputHandler(ChatBotController controller)
    {
        controller.AddInputHandler(
            new Regex(@"(time)|(время)|(который час)|(который час)"),
            GetTime);

        controller.AddInputHandler(
            new Regex(@"(date)|(дата)|(какая сегодня дата)"),
            GetDate);

        controller.AddInputHandler(
            new Regex(@"(calculate)|(compute)|(вычисли)|(посчитай)"),
            CalculateExpressionFromString);

        controller.AddInputHandler(
            new Regex(@"(currency rate)|(курс валют)|(курс)"),
            GetCurrencyRate);
    }


    protected override void OnClosing(CancelEventArgs e)
    {
        string path = "messages.xml";
        string xml =
            ObjectSerializerToXml<ObservableCollection<Message>>.ToXml(chatHistory);

        using (StreamWriter writer = new(path, false))
        {
            writer.WriteLine(xml);
        }
    }
}
