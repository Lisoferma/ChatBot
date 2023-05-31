namespace ChatBot;

public interface IChatHistoryStorage
{
    void Save(ICollection<Message> messages);

    void Load(ICollection<Message> messages);
}
