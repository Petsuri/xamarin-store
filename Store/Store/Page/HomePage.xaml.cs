

using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Store.Domain;

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

        private async void ImageCell_Tapped(object sender, System.EventArgs e)
        {
            IsPresented = false;
            await m_navigation.PushAsync(new PurchasedBooksPage());
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
