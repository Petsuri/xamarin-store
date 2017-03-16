using Store.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Practices.Unity;
using Store.Model;

namespace Store.Ui.View
{

    public partial class PurchasedBooksPage : ContentPage
    {

        private const int BooksPerRow = 4;

        public PurchasedBooksPage()
        {
            InitializeComponent();

            showPurchasedBooks();

        }

        private async void showPurchasedBooks()
        {
            var repository = App.Container.Resolve<IPurchasedBooksRepository>();
            var purchasedBooks = await repository.loadAllAsync();
            var allBooks = purchasedBooks.ToList();

            if (allBooks.Any())
            {
                display(allBooks);
            }else
            {
                var noBooks = new Label() { Text = "Ei ostettuja kirjoja" };
                noBooks.HorizontalTextAlignment = TextAlignment.Center;

                Grid.SetRow(noBooks, 0);
                Grid.SetColumn(noBooks, 0);
                
                booksGrid.Children.Add(noBooks);
            }
            
        }

        private void display(IList<Book> books)
        {
            booksGrid.RowDefinitions.Clear();
            booksGrid.ColumnDefinitions.Clear();

            var requiredNumberOfRows = (books.Count() / BooksPerRow) + 1;
            for(var rowIndex = 0; rowIndex < requiredNumberOfRows; rowIndex++)
            {
                booksGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            }
            booksGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            for (var bookIndex = 0; bookIndex < books.Count(); bookIndex++)
            {
                var currentBook = books[bookIndex];

                var page = new StoreItemPreviewView();
                page.BindingContext = currentBook;

                var row = (bookIndex / BooksPerRow);
                var column = (bookIndex % BooksPerRow);

                Grid.SetRow(page, row);
                Grid.SetColumn(page, column);

                booksGrid.Children.Add(page);
            }
        }
    }
}
