

using Xamarin.Forms;
using Microsoft.Practices.Unity;
using System;
using Store.Interface.Repository;
using Store.Interface.Platform;

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

        private void CloseProgram(object sender, EventArgs e)
        {
            var application = App.Container.Resolve<IApplication>();
            application.Close();
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

        private async void GivePurchasedBooksAway(object sender, EventArgs e)
        {
            IsPresented = false;

            var repository = App.Container.Resolve<IPurchasedBooksRepository>();
            await repository.RemoveAllAsync();

        }

        private async void ClearWishList(object sender, EventArgs e)
        {
            IsPresented = false;

            var repository = App.Container.Resolve<IWishListRepository>();
            await repository.RemoveAllAsync();
        }
    }
}
