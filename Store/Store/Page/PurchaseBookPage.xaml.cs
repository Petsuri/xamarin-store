
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Store.ViewModel;
using Store.Model;

namespace Store.Ui.Page
{

    public partial class PurchaseBookPage : ContentPage
    {

        private PurchaseBookViewModel m_model;
        public int BookId { get; private set; }

        
        public PurchaseBookPage(int bookId, BookCategory.Category category)
        {
            InitializeComponent();
            this.BookId = bookId;
            
            m_model = App.Container.Resolve<PurchaseBookViewModel>(category.ToString());
            BindingContext = m_model;

        }

        protected override void OnAppearing()
        {
            
            base.OnAppearing();
            m_model.Load(BookId);

        }
    }
}
