
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Store.ViewModel;
using Store.Model;

namespace Store.Ui.Page
{

    public partial class BookPage : ContentPage
    {

        private BookViewModel m_model;
        public int BookId { get; private set; }

        
        public BookPage(int bookId, BookCategory.Category category)
        {
            InitializeComponent();
            this.BookId = bookId;
            
            m_model = App.Container.Resolve<BookViewModel>(category.ToString());
            BindingContext = m_model;

        }

        protected override void OnAppearing()
        {
            
            base.OnAppearing();
            m_model.Load(BookId);

        }
    }
}
