using Store.Domain;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void addRecommendationList(BookPreviewItemListViewModel r)
        {
            Recommendation = r;
        }

        public void addMangaList(BookPreviewItemListViewModel m)
        {
            Manga = m;
        }
        
        public void loadItems()
        {
            IsBusy = true;
            Manga.loadNextBooks();
            Recommendation.loadNextBooks();
            IsBusy = false;
        }

        public bool IsBusy
        {
            get { return m_isBusy; }
            set { SetProperty(ref m_isBusy, value); }
        }

        public void disableSelections()
        {
            Manga.disableSelection();
            Recommendation.disableSelection();
        }

        public void enableSelections()
        {
            Manga.enableSelection();
            Recommendation.enableSelection();
        }
        
        public BookPreviewItemListViewModel Manga { get; private set; }
        public BookPreviewItemListViewModel Recommendation { get; private set; }

    }
}
