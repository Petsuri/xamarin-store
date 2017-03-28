
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Store.ViewModel;

namespace Store.Ui.Page
{

    public partial class PurchaseBookPage : ContentPage
    {
        private bool m_isBookLoaded = false;
        private PurchaseBookViewModel m_model;
        public int BookId { get; private set; }

        
        public PurchaseBookPage(int bookId)
        {
            InitializeComponent();
            this.BookId = bookId;
            
            m_model = App.Container.Resolve<PurchaseBookViewModel>();
            BindingContext = m_model;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!m_isBookLoaded)
            {
                m_model.Load(BookId);
                m_isBookLoaded = true;
            
            }


        }
    }
}
