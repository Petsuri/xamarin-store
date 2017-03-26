using Store.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Interface.Repository
{
    public interface IPurchasedBooksRepository
    {

        Task AddAsync(Book book);
        Task<IEnumerable<Book>> LoadAllAsync();
        Task<bool> IsPurchasedAsync(int bookId);

        Task RemoveAllAsync();

    }
}
