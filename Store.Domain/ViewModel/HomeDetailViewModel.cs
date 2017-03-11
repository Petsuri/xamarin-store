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
               

        public HomeDetailViewModel(IMessageQueue messaging)
        {
            m_messaging = messaging;
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

            Recommendation.loadNextBooks();
            Manga.loadNextBooks();

        }
        
        public BookPreviewItemListViewModel Recommendation { get; private set; }
        public BookPreviewItemListViewModel Manga { get; private set; }

    }
}
