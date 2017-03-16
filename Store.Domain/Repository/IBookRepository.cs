using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public interface IBookRepository
    {

        Task<IEnumerable<int>> loadAllPreviewItemIdsAsync();
        Task<IEnumerable<BookPreviewItem>> loadPreviewItemsAsync(int startFrom, int takeCount);

        Task<Book> loadAsync(int id);

    }
}
