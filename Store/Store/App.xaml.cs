using Microsoft.Practices.Unity;
using Store.Domain;
using Store.Ui.Common;
using Store.Ui.View;

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
            container.RegisterType<IBookRepository, DataMock.BookRepository>();
            container.RegisterType<IMessageQueue, XamarinMessageQueue>();
            container.RegisterType<IReviewRepository, DataMock.BookReviewRepository>();
            container.RegisterType<ICamera, Camera>();
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
