using Microsoft.Practices.Unity;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Store.ViewModel;

namespace Store.Ui.View
{
    public partial class HomeMasterPage : ContentPage
    {
        public HomeMasterPage()
        {
            InitializeComponent();

            BindingContext = App.Container.Resolve<HomeMasterViewModel>();
        }
    }
}
