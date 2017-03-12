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

        public void add(Book book)
        {
            m_purchasedBooks.Add(book);
        }

        public IEnumerable<Book> loadAll()
        {
            return m_purchasedBooks;
        }
    }
}
