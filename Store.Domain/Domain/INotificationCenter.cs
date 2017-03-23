namespace Store.Domain
{
    public interface INotificationCenter
    {
        void Publish(string title, string message);
    }
}
