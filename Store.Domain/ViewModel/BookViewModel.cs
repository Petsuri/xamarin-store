using Store.Domain;
using Store.Model;
using Store.Repository;
using Store.Service;
using System.Windows.Input;

namespace Store.ViewModel
{
    public class BookViewModel : ViewModelBase
    {

        public const string ShowBookCoverMessage = "showShowBookCover";
        
        private bool m_isBusy = false;
        private int? m_currentBookId = null;

        private WriteReviewViewModel m_newReview;
        private IBookRepository m_booksRepository;
        private IReviewRepository m_reviewRepository;
        private Book m_book;

        public BookViewModel(IBookRepository bookRepository, 
                             IReviewRepository reviewRepository, 
                             IMessageQueue messaging, 
                             WriteReviewViewModel newReview,
                             PurchaseBookService purchaseService)
        {
            m_booksRepository = bookRepository;
            m_reviewRepository = reviewRepository;
            NewReview = newReview;
            Reviews = new ObservableRangeCollection<Review>();
            ShowBookCover = new Command(
                execute: () =>
            {
                messaging.Send(this, ShowBookCoverMessage, Book.Image);
            }, canExecute: () =>
            {
                return Book != null && Book.Image != null;
            }
            );

            SubmitNewReview = new Command(
                execute: SubmitReview,
                canExecute: () =>
            {
                return NewReview.IsValid() && m_currentBookId.HasValue;
            });

            BuyBook = new Command(
                execute: () =>
            {
                purchaseService.Purchase(Book);
                IncreaseBookPurchasedCountBy(1);

                ChangeCanCommanExecute(BuyBook);

            }, canExecute: () =>
            {
                return Book != null && purchaseService.IsMoneyEnoughForPurchase(Book);
            });

            newReview.PropertyChanged += (sender, args) =>
            {
                ChangeCanCommanExecute(SubmitNewReview);
            };
        }

        private async void SubmitReview()
        {
            var review = NewReview.GetReview();
            await m_reviewRepository.SaveReviewAsync(m_currentBookId.Value, review);

            Reviews.Add(review);

            NewReview.Clear();
        }

        private void ChangeCanCommanExecute(ICommand command)
        {
            var c = (command as Command);
            if (c != null)
            {
                c.ChangeCanExecute();
            }


        }
        
        private void IncreaseBookPurchasedCountBy(int amount)
        {

            var currentBook = Book;
            currentBook.PurchasedCount += amount;

            Book = null;
            Book = currentBook;

        }

        public async void Load(int bookId)
        {
            bool loadSelectedBook = (!m_currentBookId.HasValue || (m_currentBookId.Value != bookId));
            if (loadSelectedBook)
            {
                m_currentBookId = bookId;
                ChangeCanCommanExecute(SubmitNewReview);

                IsBusy = true;
                
                Book = await m_booksRepository.LoadAsync(bookId);
                ChangeCanCommanExecute(BuyBook);
                ChangeCanCommanExecute(ShowBookCover);

                var bookReviews = await m_reviewRepository.LoadReviewsAsync(bookId);
                Reviews.Clear();
                Reviews.AddRange(bookReviews);

                IsBusy = false;
            }
        }

        public Book Book
        {
            get { return m_book;}
            set { SetProperty(ref m_book, value); }
        }

        public bool IsBusy
        {
            get { return m_isBusy; }
            set { SetProperty(ref m_isBusy, value); }
        }

       
        public WriteReviewViewModel NewReview
        {
            get { return m_newReview; }
            set { SetProperty(ref m_newReview, value); }
        }

        public ObservableRangeCollection<Review> Reviews { get; private set; }
        public ICommand ShowBookCover { get; private set; }
        public ICommand SubmitNewReview { get; private set; }
        public ICommand BuyBook { get; private set; }

    }
}
