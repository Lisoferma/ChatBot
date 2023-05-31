using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

    private List<Message> chatHistory;
    private readonly ChatBotController chatbot;


    public MainWindow()
    {
        DataContext = this;
        Username = "User";
        chatHistory = new List<Message>();
        chatbot = new ChatBotController();

        InitializeInputHandler();
        InitializeComponent();           
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

        TextBlock_chat.Text += $"{Username}: {input}\n";
        chatHistory.Add(userMessage);

        string answer = chatbot.GetAnswer(input);

        Message botMessage = new()
        {
            Text = answer,
            Author = BotName,
            TimeStamp = DateTime.Now
        };

        TextBlock_chat.Text += $"{BotName}: {answer}\n";
        ScrollViewer_chatScroll.ScrollToEnd();
        chatHistory.Add(botMessage);
    }


    private void InitializeInputHandler()
    {
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
    }


    protected override void OnClosing(CancelEventArgs e)
    {
        string path = "messages.xml";
        string xml =
            ObjectSerializerToXml< List<Message> >.ToXml(chatHistory);

        using (StreamWriter writer = new(path, false))
        {
            writer.WriteLine(xml);
        }
    }
}
