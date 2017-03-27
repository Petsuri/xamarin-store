using Store.Interface.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.LocalDatabase.Connection;
using Store.LocalDatabase.Entity;

namespace Store.LocalDatabase.Repository
{
    public class PersistentWishListRepository : IWishListRepository
    {

        private IDatabase m_database;
        private IBookRepository m_bookRepository;

        public PersistentWishListRepository(IDatabase database, IBookRepository bookRepository)
        {
            m_database = database;
            m_bookRepository = bookRepository;
        }

        public async Task AddAsync(Model.Book selectedBook)
        {
            var item = new Book() { BookId = selectedBook.Id, BookType = Book.UserLibraryType.InWishList };
            await m_database.InsertAsync(item);
        }

        public async Task<bool> IsInWishListAsync(int bookId)
        {
            return (await LoadAllWishListBookIdsAsync()).Any(id => id == bookId);
        }

        public async Task<IEnumerable<Model.Book>> LoadAllAsync()
        {
            var allWishListBookIds = await LoadAllWishListBookIdsAsync();
            return await m_bookRepository.LoadAsync(allWishListBookIds);
        }
        
        public async Task RemoveAsync(Model.Book selectedBook)
        {
            var foundBooks = (await LoadAllWishListBooksAsync()).Where(item => item.BookId == selectedBook.Id);
            foreach (var book in foundBooks)
            {
                await m_database.DeleteAsync(book);
            }
        }

        public async Task RemoveAllAsync()
        {
            var removeBooks = await LoadAllWishListBooksAsync();
            foreach(var book in removeBooks)
            {
                await m_database.DeleteAsync(book);
            }
        }

        private async Task<IEnumerable<int>> LoadAllWishListBookIdsAsync()
        {
            return (await LoadAllWishListBooksAsync()).Select(item => item.BookId);
        }

        private async Task<IEnumerable<Book>> LoadAllWishListBooksAsync()
        {
            return (await m_database.LoadAllAsync<Book>()).Where(item => item.BookType == Book.UserLibraryType.InWishList);
        }
        
    }
}
