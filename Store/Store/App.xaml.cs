using Microsoft.Practices.Unity;
using Store.Interface.Domain;
using Store.Interface.Platform;
using Store.Interface.Repository;
using Store.Model;
using Store.Service;
using Store.Ui.Common;
using Store.Ui.Page;
using Store.ViewModel;
using Xamarin.Forms;

namespace Store.Ui
{
    public partial class App : Application
    {

        public static IUnityContainer Container { get; private set; }
        
        public App()
        {
            InitializeComponent();

            Container = new UnityContainer();
            RegisterDependencies(Container);
            var messaging = Container.Resolve<IMessageQueue>();
            messaging.Subscribe<PurchaseBookViewModel, Book>(this, PurchaseBookViewModel.BookPurchased, ShowBookPurchasedNotification);


            MainPage = new HomePage();


        }

        private void ShowBookPurchasedNotification(PurchaseBookViewModel sender, Book purchasedBook)
        {
            var notifications = Container.Resolve<INotifications>();
            notifications.BookPurchased(purchasedBook);
        }

        private static void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterType<IMessageQueue, XamarinMessageQueue>();

            container.RegisterType<IWallet, DataMock.Wallet>();
            container.RegisterType<IReviewRepository, DataMock.BookReviewRepository>();
            container.RegisterType<IWishListRepository, DataMock.WishListRepository>();
            container.RegisterType<IPurchasedBooksRepository, DataMock.PurchasedBooksRepository>();
            container.RegisterType<IBookRepository, DataMock.MangaBookRepository>(BookCategory.Category.Manga.ToString());
            container.RegisterType<IBookRepository, DataMock.RecommendationBookRepository>(BookCategory.Category.Recommendation.ToString());

            container.RegisterType<PurchaseBookViewModel>(BookCategory.Category.Recommendation.ToString(),
                new InjectionConstructor(
                    typeof(DataMock.RecommendationBookRepository), 
                    typeof(DataMock.BookReviewRepository), 
                    typeof(XamarinMessageQueue), 
                    typeof(DataMock.WishListRepository),
                    typeof(WriteReviewViewModel),
                    typeof(PurchaseBookService)
                )
            );


            container.RegisterType<PurchaseBookViewModel>(BookCategory.Category.Manga.ToString(),
                new InjectionConstructor(
                    typeof(DataMock.MangaBookRepository),
                    typeof(DataMock.BookReviewRepository),
                    typeof(XamarinMessageQueue),
                    typeof(DataMock.WishListRepository),
                    typeof(WriteReviewViewModel),
                    typeof(PurchaseBookService)
                )
            );

            container.RegisterType<BookPreviewListViewModel>(BookCategory.Category.Recommendation.ToString(),
                new InjectionConstructor(
                    typeof(DataMock.RecommendationBookRepository),
                    typeof(IMessageQueue)
                )
            );

            container.RegisterType<BookPreviewListViewModel>(BookCategory.Category.Manga.ToString(),
                new InjectionConstructor(
                    typeof(DataMock.MangaBookRepository),
                    typeof(IMessageQueue)
                )   
            );

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
