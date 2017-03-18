using Store.Domain;

namespace Store.ViewModel
{
    public class HomeDetailViewModel : ViewModelBase
    {


        private readonly IMessageQueue m_messaging;
        private bool m_isBusy;
               

        public HomeDetailViewModel(IMessageQueue messaging)
        {
            m_messaging = messaging;
            m_isBusy = false;
        }

        public void AddRecommendationList(BookPreviewListViewModel r)
        {
            Recommendation = r;
        }

        public void AddMangaList(BookPreviewListViewModel m)
        {
            Manga = m;
        }
        
        public void LoadItems()
        {
            IsBusy = true;
            Manga.LoadNextBooks();
            Recommendation.LoadNextBooks();
            IsBusy = false;
        }

        public bool IsBusy
        {
            get { return m_isBusy; }
            set { SetProperty(ref m_isBusy, value); }
        }

        public void DisableSelections()
        {
            Manga.DisableSelection();
            Recommendation.DisableSelection();
        }

        public void EnableSelections()
        {
            Manga.EnableSelection();
            Recommendation.EnableSelection();
        }
        
        public BookPreviewListViewModel Manga { get; private set; }
        public BookPreviewListViewModel Recommendation { get; private set; }

    }
}
