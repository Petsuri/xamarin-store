using Store.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository
{
    public interface IPurchasedBooksRepository
    {

        Task AddAsync(Book book);
        Task<IEnumerable<Book>> LoadAllAsync();

    }
}
