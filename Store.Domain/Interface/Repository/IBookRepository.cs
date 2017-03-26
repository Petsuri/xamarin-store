using Store.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Interface.Repository
{
    public interface IBookRepository
    {

        Task<IEnumerable<int>> LoadAllPreviewBookIdsAsync(BookCategory.Category category);
        Task<IEnumerable<BookPreview>> LoadPreviewBookAsync(int startFrom, int takeCount, BookCategory.Category category);

        Task<Book> LoadAsync(int id);
        Task<IEnumerable<Book>> LoadAsync(IEnumerable<int> ids);

    }
}
