
using Store.Model;
using Store.ViewModel;
using Microsoft.Practices.Unity;
using System.Collections.Specialized;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;
using Store.Domain;

namespace Store.Ui.View
{
    
    public partial class HomeDetailPage : ContentPage
    {
        private HomeDetailViewModel m_home;
        
        public HomeDetailPage()
        {
            InitializeComponent();

            m_home = App.Container.Resolve<HomeDetailViewModel>();
            BindingContext = m_home;

            IBookRepository books = App.Container.Resolve<IBookRepository>();
            IMessageQueue messaging = App.Container.Resolve<IMessageQueue>();
            messaging.Subscribe<BookPreviewItemViewModel, BookPreviewItem>(this, BookPreviewItemViewModel.ShowItemMessage, async (sender, selectedItem) =>
            {

                var allPreviewIds = await books.loadAllPreviewItemIds();
                await Navigation.PushAsync(new BookCarouselPage(selectedItem.Id, allPreviewIds));

            });

            messaging.Subscribe<BookPreviewItemViewModel, BookPreviewItem>(this, BookPreviewItemViewModel.ShowOptionsMessage, async (sender, selectedItem) =>
            {

                var camera = App.Container.Resolve<ICamera>();
                if (camera.isTakePhotoSupported())
                {
                    await camera.takePhoto();

                }

                string[] buttons = { "Katso", "Ei kiinnosta" };
                var selectedAction = await DisplayActionSheet("Toiminnot", null, null, buttons);
                
            });
            
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            m_home.loadNextBooks();
        }

        private void BookRecommendationsScrolled(object sender, ScrolledEventArgs e)
        {
            var currentScrollView = (ScrollView)sender; 

            bool isScrolledToRightEnd = (currentScrollView.ScrollX >= (currentScrollView.ContentSize.Width - currentScrollView.Width));
            if (isScrolledToRightEnd)
            {
                m_home.loadNextBooks();
            }


        }
                
    }
    
}
