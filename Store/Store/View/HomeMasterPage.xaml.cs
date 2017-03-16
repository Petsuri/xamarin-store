using Microsoft.Practices.Unity;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Store.ViewModel;
using System;

namespace Store.Ui.View
{
    public partial class HomeMasterPage : ContentPage
    {
        private INavigation m_navigation;

        public HomeMasterPage(INavigation navigation)
        {
            InitializeComponent();
            m_navigation = navigation;

            BindingContext = App.Container.Resolve<HomeMasterViewModel>();
        }

        private async void ImageCell_Tapped(object sender, System.EventArgs e)
        {
            await m_navigation.PushAsync(new PurchasedBooksPage());    
        }
    }
}
