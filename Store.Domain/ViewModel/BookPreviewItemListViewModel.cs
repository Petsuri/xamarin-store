using Store.Domain;
using Store.Model;
using Store.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Store.ViewModel
{
    public class BookPreviewItemListViewModel : ViewModelBase
    {

        private const int LoadItemsCount = 5;


        private bool m_isBusy;
        private bool m_searchForMoreBooks;
        private int m_currentBookItemIndex;

        private readonly IMessageQueue m_messaging;
        private readonly IBookRepository m_bookStore;

        public BookPreviewItemListViewModel(IBookRepository bookStore, IMessageQueue messaging)
        {
            m_searchForMoreBooks = true;
            m_currentBookItemIndex = 0;

            m_bookStore = bookStore;
            m_messaging = messaging;
            
            Books = new ObservableRangeCollection<BookPreviewItemViewModel>();

        }


        public async void loadNextBooks()
        {
            if (!m_searchForMoreBooks || IsBusy)
            {
                return;
            }

            IsBusy = true;

            IEnumerable<BookPreviewItem> loadedBooks = await m_bookStore.loadPreviewItems(m_currentBookItemIndex, LoadItemsCount);
            m_currentBookItemIndex += loadedBooks.Count();
            m_searchForMoreBooks = (loadedBooks.Count() == LoadItemsCount);


            var bookViewModels = new List<BookPreviewItemViewModel>();
            foreach (var book in loadedBooks)
            {
                bookViewModels.Add(new BookPreviewItemViewModel(book, m_messaging));
            }

            Books.AddRange(bookViewModels);

            IsBusy = false;

        }

        public bool IsBusy
        {
            get { return m_isBusy; }
            set { SetProperty(ref m_isBusy, value); }
        }
        public ObservableRangeCollection<BookPreviewItemViewModel> Books { get; private set; }

    }
}
