using CommunityToolkit.Mvvm.Messaging;

namespace ToDoAppBL.Services
{
    public class MessageService
    {
        public void Register<T>(object recipient, MessageHandler<object, T> handler)
         where T : class
        {
            WeakReferenceMessenger.Default.Register(recipient, handler);
        }

        public void Send<T>(T message)
            where T : class
        {
            WeakReferenceMessenger.Default.Send(message);
        }
    }
}
