using System.Collections.ObjectModel;

namespace ChatBot;

public interface IMessagesStorage
{
    void Save(ObservableCollection<Message> messages);

    void Load(ObservableCollection<Message> messages);
}
