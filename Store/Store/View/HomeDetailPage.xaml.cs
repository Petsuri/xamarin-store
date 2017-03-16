
using Store.Model;
using Store.ViewModel;
using Microsoft.Practices.Unity;

using Xamarin.Forms;
using Store.Domain;
using Store.Repository;

namespace Store.Ui.View
{

    public partial class HomeDetailPage : ContentPage
    {
        private HomeDetailViewModel m_viewModel;
        
        public HomeDetailPage()
        {
            InitializeComponent();
            
            m_viewModel = App.Container.Resolve<HomeDetailViewModel>();
            m_viewModel.addMangaList(App.Container.Resolve<BookPreviewItemListViewModel>(BookCategory.Category.Manga.ToString()));
            m_viewModel.addRecommendationList(App.Container.Resolve<BookPreviewItemListViewModel>(BookCategory.Category.Recommendation.ToString()));
            m_viewModel.loadItems();
            
            BindingContext = m_viewModel;

            IMessageQueue messaging = App.Container.Resolve<IMessageQueue>();
            messaging.Subscribe<BookPreviewItemViewModel, BookPreviewItem>(this, BookPreviewItemViewModel.ShowItemMessage, async (sender, selectedItem) =>
            {
                m_viewModel.IsBusy = true;
                m_viewModel.disableSelections();

                var books = App.Container.Resolve<IBookRepository>(selectedItem.Category.SelectedCategory.ToString());
                var allPreviewIds = await books.loadAllPreviewItemIdsAsync();
                await Navigation.PushAsync(new BookCarouselPage(selectedItem.Id, allPreviewIds, selectedItem.Category.SelectedCategory));

                m_viewModel.IsBusy = false;
                m_viewModel.enableSelections();
            });
            
        }
       

        private void BookRecommendationsScrolled(object sender, ScrolledEventArgs e)
        {            
            if (isScrollectToRightEnd((ScrollView)sender))
            {
                m_viewModel.Recommendation.loadNextBooks();
            }
        }

        private void BookMangasScrolled(object sender, ScrolledEventArgs e)
        {
            if (isScrollectToRightEnd((ScrollView)sender))
            {
                m_viewModel.Manga.loadNextBooks();
            }
        }


        private bool isScrollectToRightEnd(ScrollView sv)
        {
            return (sv.ScrollX >= (sv.ContentSize.Width - sv.Width));
        }

    }
    
}
