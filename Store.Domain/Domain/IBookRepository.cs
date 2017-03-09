using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain
{
    public interface IBookRepository
    {

        Task<IEnumerable<int>> loadAllPreviewItemIds();
        Task<IEnumerable<BookPreviewItem>> loadPreviewItems(int startFrom, int takeCount);

        Task<Book> load(int id);

    }
}
