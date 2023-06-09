﻿using System.Text.RegularExpressions;

namespace ChatBot;

/// <summary>
/// Контроллер для обработки сообщений пользователя.
/// Содержит список команд для пользователя и текущий чатбот.
/// </summary>
public class ChatBotController : IBotResponse
{
    /// <summary>
    /// Чатбот от которого пользователь будет получать ответ.
    /// </summary>
    public IBotResponse Chatbot { get; set; }

    /// <summary>
    /// Делегат для методов обработки сообщений и команд пользователя.
    /// </summary>
    /// <param name="input">Сообщение от пользователя.</param>
    /// <returns>Ответ на сообщение.</returns>
    public delegate string InputHandler(string input);

    // Обработчики сообщений и regex который их вызывает.
    private Dictionary<Regex, InputHandler> _dictInputHandlers;


    /// <summary>
    /// Инициализирует чатбот как ChatBotAIML.
    /// </summary>
    public ChatBotController() : this(new ChatBotAIML()) { }


    /// <summary>
    /// Инициализировать и задать чатбот. 
    /// </summary>
    /// <param name="chatBot">Чатбот который будет отвечать на сообщения.</param>
    public ChatBotController(IBotResponse chatBot)
    {       
        Chatbot = chatBot;
        _dictInputHandlers = new Dictionary<Regex, InputHandler>();
    }


    /// <summary>
    /// Получить ответ на сообщение от подходящего обработчика
    /// или, если его нет, получить ответ от чатбота.
    /// </summary>
    /// <param name="input">Сообщение от пользователя.</param>
    /// <returns>Ответ на сообщение.</returns>
    public string GetAnswer(string input)
    {
        input = input.ToLower();

        // Поиск подходящего обработчика сообщения, если не найден - null.
        InputHandler? foundHandler = 
            _dictInputHandlers.FirstOrDefault(handler => handler.Key.IsMatch(input)).Value;

        if (foundHandler != null)
            return foundHandler(input);
        else
            return Chatbot.GetAnswer(input);
    }


    /// <summary>
    /// Добавить обработчик сообщения пользователя.
    /// </summary>
    /// <param name="regex">Regex который будет вызывать обработчик.</param>
    /// <param name="inputHandler">Обработчик сообщения.</param>
    public void AddInputHandler(Regex regex, InputHandler inputHandler)
    {
        _dictInputHandlers.Add(regex, inputHandler);
    }
}

