using System.Collections.ObjectModel;

namespace ChatBot;

public class MessagesStorageInXml : IMessagesStorage
{
    public void Save(ObservableCollection<Message> messages)
    {
        throw new NotImplementedException();
    }


    public void Load(ObservableCollection<Message> messages)
    {
        throw new NotImplementedException();
    }
}
