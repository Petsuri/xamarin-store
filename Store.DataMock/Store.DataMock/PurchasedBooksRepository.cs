using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Model;
using System.Linq;
using Store.Interface.Repository;

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

        public async Task<bool> IsPurchasedAsync(int bookId)
        {
            await Task.Delay(100);
            return m_purchasedBooks.Any(book => book.Id == bookId);
        }

        public async Task<IEnumerable<Book>> LoadAllAsync()
        {
            await Task.Delay(250);
            return m_purchasedBooks;
        }
    }
}
