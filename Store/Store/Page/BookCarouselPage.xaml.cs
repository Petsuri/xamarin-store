using Store.Model;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Store.ViewModel;
using Store.Interface.Domain;

namespace Store.Ui.Page
{

    public partial class BookCarouselPage : CarouselPage
    {


        public BookCarouselPage(int selectedBookId, IEnumerable<int> allBookIds, BookCategory.Category selectedBookCategory)
        {
            InitializeComponent();


            Title = (selectedBookCategory == BookCategory.Category.Manga) ? "Mangaa suoraan Jaappanista" : "Suosittelemme sinulle";
                
            SetChildrenPages(selectedBookId, allBookIds, selectedBookCategory);

            var messaging = App.Container.Resolve<IMessageQueue>();
            messaging.Subscribe<PurchaseBookViewModel, byte[]>(this, PurchaseBookViewModel.ShowBookCoverMessage, async (sender, imageBytes) =>
            {
                await Navigation.PushAsync(new BookCoverPage(imageBytes));
            });
        }

        private void SetChildrenPages(int selectedBookId, IEnumerable<int> allBookIds, BookCategory.Category selectedBookCategory)
        {

            IList<PurchaseBookPage> allBookPages = new List<PurchaseBookPage>();
            foreach (var id in allBookIds)
            {
                allBookPages.Add(new PurchaseBookPage(id, selectedBookCategory));
            }

            foreach (var page in allBookPages)
            {
                Children.Add(page);
            }

            SelectedItem = allBookPages.First(b => b.BookId == selectedBookId);

        }
        
    }
}
