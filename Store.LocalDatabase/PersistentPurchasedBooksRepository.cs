using Store.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.LocalDatabase.Connection;
using Store.Interface.Platform;
using System.Linq;

namespace Store.LocalDatabase
{
    public class PersistentPurchasedBooksRepository : IPurchasedBooksRepository
    {

        private IDatabase<Book> m_database;
        private IBookRepository m_bookRepository;

        public PersistentPurchasedBooksRepository(IDatabase<Book> database, IBookRepository bookRepository)
        {
            m_database = database;
            m_bookRepository = bookRepository;
        }
        
        public async Task AddAsync(Model.Book book)
        {
            var item = new Book() { BookId = book.Id, BookType = Book.UserLibraryType.Purchased };
            await m_database.InsertAsync(item);

        }

        public async Task<bool> IsPurchasedAsync(int bookId)
        {
            return (await LoadAllPurchasedBookIdsAsync()).Any(id => id == bookId);   
        }

        public async Task<IEnumerable<Model.Book>> LoadAllAsync()
        {
            var allPurchasedBookIds = await LoadAllPurchasedBookIdsAsync();
            return await m_bookRepository.LoadAsync(allPurchasedBookIds);
        }

        private async Task<IEnumerable<int>> LoadAllPurchasedBookIdsAsync()
        {
            return (await LoadAllPurchasedBooksAsync()).Select(item => item.BookId);
        }

        public async Task RemoveAllAsync()
        {
            var allBooks = await LoadAllPurchasedBooksAsync();
            foreach(var book in allBooks)
            {
                await m_database.DeleteAsync(book.Id);
            }
        }

        private async Task<IEnumerable<Book>> LoadAllPurchasedBooksAsync()
        {
            return (await m_database.LoadAllAsync()).Where(item => item.BookType == Book.UserLibraryType.Purchased);
        }

    }
}
