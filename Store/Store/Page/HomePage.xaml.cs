

using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Store.Domain;
using Store.Repository;
using System.Linq;
using System;

namespace Store.Ui.Page
{
    public partial class HomePage : MasterDetailPage
    {

        private INavigation m_navigation;
        
        public HomePage()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new HomeDetailPage());

            Detail = navigationPage;
            m_navigation = navigationPage.Navigation;
            
        }

        private async void ShowOwnBooks(object sender, EventArgs e)
        {
            IsPresented = false;


            var repository = App.Container.Resolve<IPurchasedBooksRepository>();
            var purchasedBooks = await repository.LoadAllAsync();
            
            await m_navigation.PushAsync(new UserBookListPage(purchasedBooks) { Title = "Omat kirjat" });

        }

        private async void ShowWishListBooks(object sender, EventArgs e)
        {
            IsPresented = false;

            var repository = App.Container.Resolve<IWishListRepository>();
            var wishListBooks = await repository.LoadAllAsync();

            await m_navigation.PushAsync(new UserBookListPage(wishListBooks) { Title = "Toivelista" });

        }

        protected override bool OnBackButtonPressed()
        {

            var isClosingHomePage = (Detail.Navigation.NavigationStack.Count == 1);
            if (isClosingHomePage)
            {
                var application = App.Container.Resolve<IApplication>();
                application.Close();
                return true;

            }else
            {
                return base.OnBackButtonPressed();
            }
                        
        }
        
    }
}
