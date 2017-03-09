

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

            Detail = new NavigationPage(new HomeDetailPage());
            Master = new HomeMasterPage();

        }

        protected override bool OnBackButtonPressed()
        {

            var isClosingHomePage = (Detail.Navigation.NavigationStack.Count == 1);
            if (isClosingHomePage)
            {
                var application = App.Container.Resolve<IApplication>();
                application.close();
                return true;

            }else
            {
                return base.OnBackButtonPressed();
            }
                        
        }
        
    }
}
