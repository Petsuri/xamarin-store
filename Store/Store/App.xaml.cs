using Microsoft.Practices.Unity;
using Store.Domain;
using Store.Model;
using Store.Repository;
using Store.Service;
using Store.Ui.Common;
using Store.Ui.View;
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

            MainPage = new HomePage();


        }

        private static void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterType<IBookRepository, DataMock.RecommendationBookRepository>(BookCategory.Category.Recommendation.ToString());
            container.RegisterType<IMessageQueue, XamarinMessageQueue>();
            container.RegisterType<IReviewRepository, DataMock.BookReviewRepository>();
            container.RegisterType<ICamera, Camera>();
            container.RegisterType<IBookRepository, DataMock.MangaBookRepository>(BookCategory.Category.Manga.ToString());
            container.RegisterType<IWallet, DataMock.Wallet>();
            container.RegisterType<IPurchasedBooksRepository, DataMock.PurchasedBooksRepository>();

            container.RegisterType<BookViewModel>(BookCategory.Category.Recommendation.ToString(),
                new InjectionConstructor(
                    typeof(DataMock.RecommendationBookRepository), 
                    typeof(DataMock.BookReviewRepository), 
                    typeof(XamarinMessageQueue), 
                    typeof(WriteReviewViewModel),
                    typeof(PurchaseBookService)
                )
            );


            container.RegisterType<BookViewModel>(BookCategory.Category.Manga.ToString(),
                new InjectionConstructor(
                    typeof(DataMock.MangaBookRepository),
                    typeof(DataMock.BookReviewRepository),
                    typeof(XamarinMessageQueue),
                    typeof(WriteReviewViewModel),
                    typeof(PurchaseBookService)
                )
            );

            container.RegisterType<BookPreviewItemListViewModel>(BookCategory.Category.Recommendation.ToString(),
                new InjectionConstructor(
                    typeof(DataMock.RecommendationBookRepository),
                    typeof(IMessageQueue)
                )
            );

            container.RegisterType<BookPreviewItemListViewModel>(BookCategory.Category.Manga.ToString(),
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
