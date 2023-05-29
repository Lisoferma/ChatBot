namespace ChatBot;

/// <summary>
/// Информация о курсах валют.
/// </summary>
public class CurrencyRate
{
    /// <value>
    /// Информация о курсе валют.
    /// </value>
    public ValuteSet Valute { get; set; }


    public CurrencyRate()
    {
        Valute = new ValuteSet();
    }
}


/// <summary>
/// Определённый набор курса валют.
/// </summary>
public class ValuteSet
{
    /// <value>
    /// Информация о долларе США.
    /// </value>
    public ValuteRate USD { get; set; }

    /// <value>
    /// Информация о евро.
    /// </value>
    public ValuteRate EUR { get; set; }

    /// <value>
    /// Информация о китайском юане.
    /// </value>
    public ValuteRate CNY { get; set; }

    /// <value>
    /// Информация о японском иене.
    /// </value>
    public ValuteRate JPY { get; set; }


    public ValuteSet()
    {
        USD = new ValuteRate();
        EUR = new ValuteRate();
        CNY = new ValuteRate();
        JPY = new ValuteRate();
    }
}


/// <summary>
/// Валюта и её курс.
/// </summary>
public class ValuteRate
{
    /// <value>
    /// Название валюты.
    /// </value>
    public string Name { get; set; }

    /// <value>
    /// Значение валюты.
    /// </value>
    public float Value { get; set; }


    public ValuteRate()
    {
        Name = "Undefined";
        Value = 0;
    }
}

