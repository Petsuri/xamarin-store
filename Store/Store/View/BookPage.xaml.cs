using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Practices.Unity;
using Store.Domain;
using Store.ViewModel;

namespace Store.Ui.View
{

    public partial class BookPage : ContentPage
    {

        private BookViewModel m_model;
        public int BookId { get; private set; }

        
        public BookPage(int bookId)
        {
            InitializeComponent();
            this.BookId = bookId;

            m_model = App.Container.Resolve<BookViewModel>();
            BindingContext = m_model;

        }

        protected override void OnAppearing()
        {
            
            m_model.load(BookId);
            base.OnAppearing();

        }
    }
}
