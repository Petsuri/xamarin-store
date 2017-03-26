using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Model;
using Store.Interface.Repository;

namespace Store.DataMock
{
    public class WishListRepository : IWishListRepository
    {

        private const int NotFound = -1;

        private static IList<Book> m_wishList = new List<Book>();

        public async Task AddAsync(Book selectedBook)
        {
            await Task.Delay(250);
            m_wishList.Add(selectedBook);
        }

        public Task<bool> IsInWishListAsync(int bookId)
        {
            Task.Delay(100).Wait();
            return Task.FromResult(m_wishList.Any(book => book.Id == bookId));
        }

        public async Task<IEnumerable<Book>> LoadAllAsync()
        {
            await Task.Delay(250);
            return m_wishList;
        }

        public async Task RemoveAsync(Book selectedBook)
        {
            await Task.Delay(100);

            var isBookFound = (m_wishList.IndexOf(selectedBook) != NotFound);
            if (isBookFound)
            {
                m_wishList.Remove(selectedBook);
            }

        }
    }
}
