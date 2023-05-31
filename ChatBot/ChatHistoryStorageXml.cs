namespace ChatBot;

public class ChatHistoryStorageXml : IChatHistoryStorage
{
    private string _filepath;


    public string Filepath
    {
        get => _filepath;

        set
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                throw new ArgumentNullException(
                    nameof(_filepath), "Filepath cannot be null or empty");
            }

            _filepath = value;
        }
    }


    public ChatHistoryStorageXml()
    {
        _filepath = "Chat history";
    }


    public void Save(ICollection<Message> messages)
    {
        string xml =
            ObjectSerializerToXml<ICollection<Message>>.ToXml(messages);

        using (StreamWriter writer = new(Filepath, false))
        {
            writer.WriteLine(xml);
        }
    }


    public void Load(ICollection<Message> messages)
    {
        throw new NotImplementedException();
    }
}
