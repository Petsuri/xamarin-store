

using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Store.Domain;

namespace Store.Ui.View
{
    public partial class HomePage : MasterDetailPage
    {
        
        public HomePage()
        {
            InitializeComponent();

            var navigation = new NavigationPage(new HomeDetailPage());

            Detail = navigation;
            Master = new HomeMasterPage(navigation.Navigation);
            
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
