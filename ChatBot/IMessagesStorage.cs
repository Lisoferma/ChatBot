namespace ChatBot;

public interface IMessagesStorage
{
    void Save(ICollection<Message> messages);

    void Load(ICollection<Message> messages);
}
