using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Store.Ui.View
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
