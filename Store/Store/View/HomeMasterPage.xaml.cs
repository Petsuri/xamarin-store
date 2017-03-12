using Microsoft.Practices.Unity;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Store.ViewModel;
using System;

namespace Store.Ui.View
{
    public partial class HomeMasterPage : ContentPage
    {
        public HomeMasterPage()
        {
            InitializeComponent();

            BindingContext = App.Container.Resolve<HomeMasterViewModel>();
        }

        private async void ImageCell_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PurchasedBooksPage());    
        }
    }
}
