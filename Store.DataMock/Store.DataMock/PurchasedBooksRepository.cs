using Store.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Model;

namespace Store.DataMock
{
    public class PurchasedBooksRepository : IPurchasedBooksRepository
    {
        private static List<Book> m_purchasedBooks = new List<Book>();

        public async Task add(Book book)
        {
            await Task.Delay(250);
            m_purchasedBooks.Add(book);
        }

        public async Task<IEnumerable<Book>> loadAll()
        {
            await Task.Delay(250);
            return m_purchasedBooks;
        }
    }
}
