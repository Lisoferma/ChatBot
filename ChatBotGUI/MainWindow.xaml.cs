using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using ChatBot;
using static ChatBot.UserCommands;

namespace ChatBotGUI;

/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // Отображаемое имя пользователя.
    private string _username;

    // Отображаемое имя бота.
    public const string BotName = "Bot";

    // Путь к файлу сохранения истории чата.
    private string chatHistoryFilepath;

    /// <summary>
    /// Хранит историю сообщений чата.
    /// </summary>
    public ObservableCollection<Message> chatHistory;

    // Предоставляет методы сохранения и загрузки истории сообщений.
    private readonly IChatHistoryStorage chatHistoryStorage;

    // Чатбот который будет отвечать пользователю.
    private readonly IBotResponse chatbot;


    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        _username = "User";         
        chatbot = new ChatBotController();
        chatHistory = new ObservableCollection<Message>();

        // Получаем Username из этого окна.
        LoginWindow loginWindow = new(this);
        loginWindow.ShowDialog();

        chatHistoryFilepath = $"{Username}.xml";
        chatHistoryStorage = new ChatHistoryStorageXml(chatHistoryFilepath);

        LoadChatHistory();
        InitializeInputHandler((ChatBotController)chatbot);
    }


    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Username
    {
        get => _username;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(
                    nameof(Username), "Username cannot be null or empty");
            }

            _username = value;
        }
    }


    /// <summary>
    /// Обработчик нажатия на кнопку отправить.
    /// Вызывает метод для получения ответа от бота.
    /// </summary>
    private void Button_send_Click(object sender, RoutedEventArgs e)
    {
        GetChatbotAnswer();
    }


    /// <summary>
    /// Получить ответ от бота на основе строки
    /// из <see cref="TextBox_input"/> и вывести на экран.
    /// </summary>
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


    /// <summary>
    /// Добавляет обработчики для сообщений пользователя.
    /// </summary>
    /// <param name="controller">
    /// <see cref="ChatBotController"/> в который нужно добавить обработчики.
    /// </param>
    private static void InitializeInputHandler(ChatBotController controller)
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

    
    /// <summary>
    /// Метод вызываемый при закрытии окна.
    /// Сохраняет историю сообщений.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnClosing(CancelEventArgs e)
    {
        try
        {
            chatHistoryStorage.Save(chatHistory);
        }
        catch
        {
            MessageBox.Show("Не удалось сохранить историю сообщений");
        }    
    }


    /// <summary>
    /// Загрузить историю чата из <see cref="chatHistoryFilepath"/>.
    /// </summary>
    private void LoadChatHistory()
    {
        ObservableCollection<Message>? loadHistory =
            chatHistoryStorage.LoadFrom(chatHistoryFilepath);

        if (loadHistory != null)
            chatHistory = loadHistory;

        ListView_chat.ItemsSource = chatHistory;
        ListView_chat.ScrollIntoView(chatHistory.LastOrDefault());
    }
}
