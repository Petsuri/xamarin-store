using Microsoft.Practices.Unity;
using Store.Interface.Domain;
using Store.Interface.Platform;
using Store.Interface.Repository;
using Store.LocalDatabase.Connection;
using Store.LocalDatabase.Repository;
using Store.Model;
using Store.Service;
using Store.Ui.Common;
using Store.Ui.Page;
using Store.ViewModel;
using System;
using Xamarin.Forms;
using System.Linq;

namespace Store.Ui
{
    public partial class App : Application
    {

        public static IUnityContainer Container { get; private set; }

        private NavigationPage m_navigation;

        public App()
        {
            InitializeComponent();

            Container = new UnityContainer();
            RegisterDependencies(Container);

            var navigation = new NavigationPage();
            m_navigation = navigation;
            MainPage = new HomePage(navigation);
            
        }

   

        private static void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterType<IMessageQueue, XamarinMessageQueue>();

            container.RegisterType<IWallet, DataMock.Wallet>();
            container.RegisterType<IReviewRepository, DataMock.BookReviewRepository>();
            //container.RegisterType<IWishListRepository, DataMock.WishListRepository>();
            //container.RegisterType<IPurchasedBooksRepository, DataMock.PurchasedBooksRepository>();
            container.RegisterType<IBookRepository, DataMock.BookRepository>();
            container.RegisterType<IWishListRepository, PersistentWishListRepository>();
            container.RegisterType<IPurchasedBooksRepository, PersistentPurchasedBooksRepository>();
        }

        protected async override void OnStart()
        {
            var messaging = Container.Resolve<IMessageQueue>();
            RegisterExceptionHandling(messaging);
            RegisterNotifications(messaging);

            var service = Container.Resolve<InternetAccessService>();
            await service.RequestInternetAccess();

        }

        private void RegisterNotifications(IMessageQueue messaging)
        {
            messaging.Subscribe<PurchaseBookViewModel, Book>(
                this,
                PurchaseBookViewModel.BookPurchased,
                (sender, purchasedBook) =>
                {
                    var notifications = Container.Resolve<INotifications>();
                    notifications.BookPurchased(purchasedBook);
                });

            messaging.Subscribe<IInternetConnection>(this, "StateChanged", async (sender) =>
            {
                var service = Container.Resolve<InternetAccessService>();
                await service.RequestInternetAccess();
            });

        }

        private void RegisterExceptionHandling(IMessageQueue messaging)
        {
            messaging.Subscribe<HomePage, Exception>(this, ShowExceptionMessage);
            messaging.Subscribe<HomeDetailPage, Exception>(this, ShowExceptionMessage);

            messaging.Subscribe<PurchaseBookViewModel, Exception>(this, ShowExceptionMessage);
            messaging.Subscribe<BookPreviewListViewModel, Exception>(this, ShowExceptionMessage);
        }

        private async void ShowExceptionMessage<TSender, TArgs>(TSender sender, TArgs ex) where TSender : class
        {
            var currentPage = m_navigation.CurrentPage;
            await currentPage.DisplayAlert(
                    "Ohjelman sisäinen virhe",
                    "Ongelma on kirjattu lokiin, ja siitä on lähtenyt ilmoitus eteenpäin sähköpostilla.",
                    "OK");
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
