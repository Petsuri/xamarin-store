using Store.Domain;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Store.ViewModel
{
    public class BookViewModel : ViewModelBase
    {

        public const string ShowBookCoverMessage = "showShowBookCover";

        private byte[] m_image = null;
        private string m_name = "";
        private decimal m_userScore = 0m;

        private string m_author = "";
        private DateTime m_publishedDate = DateTime.MinValue;
        private string m_description = "";
        private decimal m_price = 0m;
        private long m_purchasedCount = 0;
        private long m_reviewerCount = 0;
        
        private bool m_isBusy = false;
        private int? m_currentBookId = null;

        private IBookRepository m_booksRepository;
        private IReviewRepository m_reviewRepository;

        public BookViewModel(IBookRepository books, IReviewRepository reviews, IMessageQueue messaging)
        {
            m_booksRepository = books;
            m_reviewRepository = reviews;
            Reviews = new ObservableRangeCollection<Review>();

            ShowBookCover = new Command(() =>
            {
                messaging.Send(this, ShowBookCoverMessage, Image);
            });

        }

        public async void load(int bookId)
        {
            bool loadSelectedBook = (!m_currentBookId.HasValue || (m_currentBookId.Value != bookId));
            if (loadSelectedBook)
            {
                m_currentBookId = bookId;
                IsBusy = true;

                var book = await m_booksRepository.load(bookId);

                Image = book.ImageFile;
                Name = book.Name;
                UserScore = book.UserScore;
                Author = book.Author;
                PublishedDate = book.PublishedDate;
                Description = book.Description;
                Price = book.Price;
                PurchasedCount = book.PurchasedCount;
                ReviewerCount = book.ReviewerCount;

                var bookReviews = await m_reviewRepository.loadReviews(bookId);
                Reviews.Clear();
                Reviews.AddRange(bookReviews);

                IsBusy = false;
            }
        }

        public bool IsBusy
        {
            get { return m_isBusy; }
            set { SetProperty(ref m_isBusy, value); }
        }

        public byte[] Image
        {
            get { return m_image; }
            set { SetProperty(ref m_image, value); }
        }

        public string Name
        {
            get { return m_name; }
            set { SetProperty(ref m_name, value); }
        }

        public decimal UserScore
        {
            get { return m_userScore; }
            set { SetProperty(ref m_userScore, value); }
        }

        public string Author
        {
            get { return m_author; }
            set { SetProperty(ref m_author, value); }
        }

        public DateTime PublishedDate
        {
            get { return m_publishedDate; }
            set { SetProperty(ref m_publishedDate, value); }
        }

        public string Description
        {
            get { return m_description; }
            set { SetProperty(ref m_description, value); }
        }

        public decimal Price
        {
            get { return m_price; }
            set { SetProperty(ref m_price, value); }
        }

        public long PurchasedCount
        {
            get { return m_purchasedCount; }
            set { SetProperty(ref m_purchasedCount, value); }
        }

        public long ReviewerCount
        {
            get { return m_reviewerCount; }
            set { SetProperty(ref m_reviewerCount, value); }
        }

        public ObservableRangeCollection<Review> Reviews { get; private set; }
        public ICommand ShowBookCover { get; private set; }

    }
}
