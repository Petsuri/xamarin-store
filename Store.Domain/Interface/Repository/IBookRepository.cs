using Store.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Interface.Repository
{
    public interface IBookRepository
    {

        Task<IEnumerable<int>> LoadAllPreviewBookIdsAsync();
        Task<IEnumerable<BookPreview>> LoadPreviewBookAsync(int startFrom, int takeCount);

        Task<Book> LoadAsync(int id);

    }
}
