using Store.Model;

namespace Store.Interface.Platform
{
    public interface INotifications
    {
        void BookPurchased(Book book);
    }
}
