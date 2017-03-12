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

        void add(Book book);
        IEnumerable<Book> loadAll();

    }
}
