using Store.Model;

namespace Store.Domain
{
    public interface INotifications
    {
        void BookPurchased(Book book);
    }
}
