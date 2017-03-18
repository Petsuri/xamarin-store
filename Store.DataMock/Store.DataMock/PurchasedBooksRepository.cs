using Store.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Model;

namespace Store.DataMock
{
    public class PurchasedBooksRepository : IPurchasedBooksRepository
    {
        private static List<Book> m_purchasedBooks = new List<Book>();

        public async Task AddAsync(Book book)
        {
            await Task.Delay(250);
            m_purchasedBooks.Add(book);
        }

        public async Task<IEnumerable<Book>> LoadAllAsync()
        {
            await Task.Delay(250);
            return m_purchasedBooks;
        }
    }
}
