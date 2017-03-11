using Store.Domain;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Practices.Unity;
using Store.ViewModel;

namespace Store.Ui.View
{
    
    public partial class BookCarouselPage : CarouselPage
    {


        public BookCarouselPage(int selectedBookId, IEnumerable<int> allBookIds, BookCategory.Category selectedBookCategory)
        {
            InitializeComponent();
            
            setChildrenPages(selectedBookId, allBookIds, selectedBookCategory);

            var messaging = App.Container.Resolve<IMessageQueue>();
            messaging.Subscribe<BookViewModel, byte[]>(this, BookViewModel.ShowBookCoverMessage, async (sender, imageBytes) =>
            {
                await Navigation.PushAsync(new BookCoverPage(imageBytes));
            });
        }

        private void setChildrenPages(int selectedBookId, IEnumerable<int> allBookIds, BookCategory.Category selectedBookCategory)
        {

            IList<BookPage> allBookPages = new List<BookPage>();
            foreach (var id in allBookIds)
            {
                allBookPages.Add(new BookPage(id, selectedBookCategory));
            }

            foreach (var page in allBookPages)
            {
                Children.Add(page);
            }

            SelectedItem = allBookPages.First(b => b.BookId == selectedBookId);

        }
        
    }
}
