
using Store.Model;
using Store.ViewModel;
using Microsoft.Practices.Unity;

using Xamarin.Forms;
using Store.Domain;
using Store.Repository;
using System;

namespace Store.Ui.Page
{

    public partial class HomeDetailPage : ContentPage
    {
        private HomeDetailViewModel m_viewModel;
        private IMessageQueue m_messaging;
        
        public HomeDetailPage()
        {
            InitializeComponent();

            m_messaging = App.Container.Resolve<IMessageQueue>();
            m_viewModel = App.Container.Resolve<HomeDetailViewModel>();
            m_viewModel.AddMangaList(App.Container.Resolve<BookPreviewListViewModel>(BookCategory.Category.Manga.ToString()));
            m_viewModel.AddRecommendationList(App.Container.Resolve<BookPreviewListViewModel>(BookCategory.Category.Recommendation.ToString()));
            m_viewModel.LoadItems();
            
            BindingContext = m_viewModel;


            
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            m_messaging.Subscribe<BookPreviewViewModel, BookPreview>(this, BookPreviewViewModel.ShowItemMessage, async (sender, selectedItem) =>
            {

                m_viewModel.IsBusy = true;
                m_viewModel.DisableSelections();

                var books = App.Container.Resolve<IBookRepository>(selectedItem.Category.SelectedCategory.ToString());
                var allPreviewIds = await books.LoadAllPreviewBookIdsAsync();
                await Navigation.PushAsync(new BookCarouselPage(selectedItem.Id, allPreviewIds, selectedItem.Category.SelectedCategory));

                m_viewModel.IsBusy = false;
                m_viewModel.EnableSelections();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            m_messaging.Unsubscribe<BookPreviewViewModel, BookPreview>(this, BookPreviewViewModel.ShowItemMessage);

        }

        private void BookRecommendationsScrolled(object sender, ScrolledEventArgs e)
        {            
            if (IsScrollectToRightEnd((ScrollView)sender))
            {
                m_viewModel.Recommendation.LoadNextBooks();
            }
        }

        private void BookMangasScrolled(object sender, ScrolledEventArgs e)
        {
            if (IsScrollectToRightEnd((ScrollView)sender))
            {
                m_viewModel.Manga.LoadNextBooks();
            }
        }


        private bool IsScrollectToRightEnd(ScrollView sv)
        {
            return (Math.Ceiling(sv.ScrollX) >= (sv.ContentSize.Width - sv.Width));
        }

    }
    
}
