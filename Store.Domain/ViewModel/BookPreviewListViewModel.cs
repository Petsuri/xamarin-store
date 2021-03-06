﻿using Store.Interface.Domain;
using Store.Interface.Repository;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.ViewModel
{
    public class BookPreviewListViewModel : ViewModelBase
    {

        private const int LoadItemsCount = 5;


        private bool m_isBusy;
        private bool m_searchForMoreBooks;
        private int m_currentBookItemIndex;

        private readonly IMessageQueue m_messaging;
        private readonly IBookRepository m_bookStore;

        public BookPreviewListViewModel(IBookRepository bookStore, IMessageQueue messaging)
        {
            m_searchForMoreBooks = true;
            m_currentBookItemIndex = 0;

            m_bookStore = bookStore;
            m_messaging = messaging;
            
            Books = new ObservableRangeCollection<BookPreviewViewModel>();

        }
        
        public async void LoadNextBooks(BookCategory.Category category)
        {
            if (!m_searchForMoreBooks || IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                IEnumerable<BookPreview> loadedBooks = await m_bookStore.LoadPreviewBookAsync(m_currentBookItemIndex, LoadItemsCount, category);
                UpdateLoadedBooks(loadedBooks);
            }
            catch (Exception ex)
            {
                m_messaging.Send(this, ex);
            }


            IsBusy = false;

        }

        private void UpdateLoadedBooks(IEnumerable<BookPreview> loadedBooks)
        {
            m_currentBookItemIndex += loadedBooks.Count();
            m_searchForMoreBooks = (loadedBooks.Count() == LoadItemsCount);


            var bookViewModels = new List<BookPreviewViewModel>();
            foreach (var book in loadedBooks)
            {
                bookViewModels.Add(new BookPreviewViewModel(book, m_messaging));
            }

            Books.AddRange(bookViewModels);
        }

        public void DisableSelection()
        {
            ChangeBooksSelectableStatus(false);
        }

        public void EnableSelection()
        {
            ChangeBooksSelectableStatus(true);
        }

        private void ChangeBooksSelectableStatus(bool isSelectable)
        {
            foreach(var book in Books)
            {
                book.IsSelectable = isSelectable;
            }
        }

        public bool IsBusy
        {
            get { return m_isBusy; }
            set { SetProperty(ref m_isBusy, value); }
        }
        public ObservableRangeCollection<BookPreviewViewModel> Books { get; private set; }

    }
}
