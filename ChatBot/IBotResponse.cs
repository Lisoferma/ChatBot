namespace ChatBot;

/// <summary>
/// Представляет метод для получения ответа от чатбота на основе переданной строки.
/// </summary>
public interface IBotResponse
{
    /// <summary>
    /// Получить ответ от бота, на основе переданной строки.
    /// </summary>
    /// <param name="input">Строка которую будет обрабатывать бот.</param>
    /// <returns>Ответ от бота.</returns>
    string GetAnswer(string input);
}

