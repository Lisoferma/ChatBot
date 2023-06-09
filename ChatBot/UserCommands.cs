﻿using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace ChatBot;

/// <summary>
/// Методы для команд пользователя.
/// </summary>
public static class UserCommands
{
    /// <summary>
    /// Получить время сейчас.
    /// </summary>
    /// <returns>Время в виде "часы:минуты".</returns>
    public static string GetTime(string input)
    {
        return DateTime.Now.ToString("HH:mm");
    }


    /// <summary>
    /// Получить сегодняшнюю дату.
    /// </summary>
    /// <returns>Дата в виде "день.месяц.год".</returns>
    public static string GetDate(string input)
    {
        return DateTime.Now.ToString("dd.MM.yy");
    }


    /// <summary>
    /// Вычислить математическое выражение из строки.
    /// </summary>
    /// <param name="input">Строка с математическими выражениями.</param>
    /// <returns>Результат выражения.</returns>
    public static string CalculateExpressionFromString(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentNullException(nameof(input), "Input cannot be null or empty");

        DataTable table = new();

        // Убрать слово перед выражением.
        input = input.Substring(input.IndexOf(' ') + 1);

        try
        {
            double result = Convert.ToDouble(table.Compute(input, string.Empty));
            return result.ToString();
        }
        catch
        {
            return "Извините, не могу разобрать. Повторите, пожалуйста, ввод.";
        }
    }


    /// <summary>
    /// Информация о курсе валют.
    /// </summary>
    /// <returns>Курс валют в виде "Название валюты курс".</returns>
    public static string GetCurrencyRate(string input)
    {
        CurrencyRate? currencyRate = RequestCurrencyRate();

        if (currencyRate == null)
            return "Не удалось узнать курс валют. Проверьте соединение с Интернетом.";

        string result =
            $"{currencyRate.Valute.USD.Name} {currencyRate.Valute.USD.Value}\n" +
            $"{currencyRate.Valute.EUR.Name} {currencyRate.Valute.EUR.Value}\n" +
            $"{currencyRate.Valute.CNY.Name} {currencyRate.Valute.CNY.Value}\n" +
            $"{currencyRate.Valute.JPY.Name} {currencyRate.Valute.JPY.Value}";

        return result;
    }


    /// <summary>
    /// Запрос информации о курсе валют.
    /// </summary>
    /// <returns>Курс валют, null если не удалось.</returns>
    private static CurrencyRate? RequestCurrencyRate()
    {
        string url = "https://www.cbr-xml-daily.ru/daily_json.js";

        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

        try
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;
            using (StreamReader streamReader = new(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            CurrencyRate? currencyRate = 
                JsonConvert.DeserializeObject<CurrencyRate>(response);

            return currencyRate;
        }
        catch
        {
            return null;
        }
    }
}
