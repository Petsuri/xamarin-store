using Store.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Interface.Repository
{
    public interface IWishListRepository
    {

        Task AddAsync(Book selectedBook);
        Task RemoveAsync(Book selectedBook);

        Task<IEnumerable<Book>> LoadAllAsync();

        Task<bool> IsInWishListAsync(int bookId);

    }
}
