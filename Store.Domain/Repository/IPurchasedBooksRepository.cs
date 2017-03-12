using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public interface IPurchasedBooksRepository
    {

        Task add(Book book);
        Task<IEnumerable<Book>> loadAll();

    }
}
