using System.Data;

namespace LisofermaChatBot;

public class UserCommands
{
    public static string GetTime(string input)
    {
        return $"{DateTime.Now:HH:mm}";
    }


    public static string CalculateExpressionFromString(string input)
    {
        DataTable table = new();

        Convert.ToDouble(table.Compute(input, string.Empty));

        input = input.Replace(" ", "");

        // Убрать слово "умножь".
        input = input.Substring(input.LastIndexOf('ь') + 1);

        // Разбить строку на две подстроки.
        string[] words =
            input.Split(new char[] { 'н', 'а' }, StringSplitOptions.RemoveEmptyEntries);

        try
        {
            int num1 = Convert.ToInt32(words[0]);
            int num2 = Convert.ToInt32(words[1]);
            return (num1 * num2).ToString();
        }
        catch
        {
            return "Извините, не могу разобрать. Повторите, пожалуйста, ввод.";
        }
    }
}
