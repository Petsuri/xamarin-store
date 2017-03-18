using System.IO;

using Xamarin.Forms;

namespace Store.Ui.Page
{
    public partial class BookCoverPage : ContentPage
    {
        public BookCoverPage(byte[] imageBytes)
        {
            InitializeComponent();
            BookCover.Source = ImageSource.FromStream(() => { return new MemoryStream(imageBytes); });

        }
    }
}
