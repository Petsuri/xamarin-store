using Store.Model;
using Store.Service;
using System.Windows.Input;
using Store.Interface.Repository;
using Store.Interface.Domain;
using System;

namespace Store.ViewModel
{
    public class PurchaseBookViewModel : ViewModelBase
    {

        public const string ShowBookCoverMessage = "Show book cover";
        public const string BookPurchased = "Book purchased";
        
        private bool m_isBusy = false;
        private int? m_currentBookId = null;
        private bool m_canAddToWishList = false; 

        private WriteReviewViewModel m_newReview;
        private IBookRepository m_booksRepository;
        private IReviewRepository m_reviewRepository;
        private IWishListRepository m_wishListRepository;
        private PurchaseBookService m_purchaseService;
        private IMessageQueue m_messaging;
       
        private Book m_book;
        private BookViewModel m_bookViewModel;

        public PurchaseBookViewModel(IBookRepository bookRepository, 
                                     IReviewRepository reviewRepository, 
                                     IMessageQueue messaging, 
                                     IWishListRepository wishListRepository,
                                     WriteReviewViewModel newReview,
                                     PurchaseBookService purchaseService)
        {
            m_booksRepository = bookRepository;
            m_reviewRepository = reviewRepository;
            m_wishListRepository = wishListRepository;
            m_purchaseService = purchaseService;
            m_messaging = messaging;

            NewReview = newReview;
            Reviews = new ObservableRangeCollection<Review>();

            PurchaseBook = CreateBuyBookCommand(purchaseService, wishListRepository, messaging);
            AddWishList = CreateAddWishListCommand(purchaseService, wishListRepository);
            ShowBookCover = CreateShowBookCoverCommand(messaging);
            SubmitNewReview = CreateSubmitNewReviewCommand(reviewRepository);
            
            newReview.PropertyChanged += (sender, args) =>
            {
                ChangeCanCommandExecute(SubmitNewReview);
            };

        }

        private ICommand CreateBuyBookCommand(PurchaseBookService purchaseService, 
                                              IWishListRepository wishListRepository,
                                              IMessageQueue messaging)
        {
            return new Command(
               execute: async () =>
               {
                   IsBusy = true;
                   try
                   {
                       purchaseService.PurchaseAsync(m_book);
                       m_bookViewModel.PurchasedCount += 1;

                       await wishListRepository.RemoveAsync(m_book);

                       ChangeCanCommandExecute(PurchaseBook);
                       UpdateCanBookBeAddedToWishList(m_book);

                       messaging.Send(this, BookPurchased, m_book);
                   }
                   catch(Exception ex)
                   {
                       m_messaging.Send(this, ex);
                   }
                   IsBusy = false;  

               }, canExecute: () =>
               {
                   return Book != null && purchaseService.IsMoneyEnoughForPurchase(m_book);
               });
        }

        private ICommand CreateAddWishListCommand(PurchaseBookService purchaseService, IWishListRepository wishListRepository)
        {
            return new Command(
                execute: async () =>
                {
                    IsBusy = true;                    
                    try
                    {                   
                        m_canAddToWishList = false;
                        ChangeCanCommandExecute(AddWishList);
                        await wishListRepository.AddAsync(m_book);                        
                    }
                    catch (Exception ex)
                    {
                        m_canAddToWishList = true;
                        ChangeCanCommandExecute(AddWishList);
                        m_messaging.Send(this, ex);
                    }
                    IsBusy = false;

                }, canExecute: () =>
                {
                    return m_canAddToWishList;
                });
        }

        private ICommand CreateShowBookCoverCommand(IMessageQueue messaging)
        {
            return new Command(
                execute: () =>
                {
                    messaging.Send(this, ShowBookCoverMessage, Book.Image);
                }, canExecute: () =>
                {
                    return Book != null && Book.Image != null;
                }
            );

        }

        private ICommand CreateSubmitNewReviewCommand(IReviewRepository reviews)
        {
            return new Command(
                execute: async () =>
                {
                    try
                    {
                        var review = NewReview.GetReview();
                        await reviews.SaveReviewAsync(m_currentBookId.Value, review);

                        Reviews.Add(review);
                        NewReview.Clear();
                    }
                    catch(Exception ex)
                    {
                        m_messaging.Send(this, ex);
                    }
                    
                },
                canExecute: () =>
                {
                    return NewReview.IsValid() && m_currentBookId.HasValue;
                });
        }
            
   
        public async void Load(int bookId)
        {
            bool loadSelectedBook = (!m_currentBookId.HasValue || (m_currentBookId.Value != bookId));
            if (loadSelectedBook)
            {                
                IsBusy = true;
                try
                {

                    var loadedBook = await m_booksRepository.LoadAsync(bookId);
                    m_currentBookId = bookId;
                    m_book = loadedBook;
                    Book = new BookViewModel(loadedBook);

                    UpdateCanBookBeAddedToWishList(m_book);

                    ChangeCanCommandExecute(PurchaseBook);
                    ChangeCanCommandExecute(ShowBookCover);
                    ChangeCanCommandExecute(SubmitNewReview);

                    var bookReviews = await m_reviewRepository.LoadReviewsAsync(bookId);
                    Reviews.Clear();
                    Reviews.AddRange(bookReviews);

                }
                catch(Exception ex)
                {
                    m_messaging.Send(this, ex);
                }                
                IsBusy = false;
            }
        }

        private async void UpdateCanBookBeAddedToWishList(Book selectedBook)
        {

            if (m_currentBookId.HasValue)
            {
                try
                {
                    var isBookInWishList = await m_wishListRepository.IsInWishListAsync(selectedBook.Id);
                    var isBookPurchased = await m_purchaseService.IsPurchasedAsync(selectedBook);

                    m_canAddToWishList = !isBookInWishList && !isBookPurchased;
                }
                catch(Exception ex)
                {
                    m_canAddToWishList = false;
                    m_messaging.Send(this, ex);
                }
            }
            else
            {
                m_canAddToWishList = false;
            }

            ChangeCanCommandExecute(AddWishList);
            
        }

        public BookViewModel Book
        {
            get { return m_bookViewModel;}
            set { SetProperty(ref m_bookViewModel, value); }
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

        public ICommand PurchaseBook { get; private set; }
        public ICommand AddWishList { get; private set; }
        public ICommand ShowBookCover { get; private set; }
        public ICommand SubmitNewReview { get; private set; }

    }
}
