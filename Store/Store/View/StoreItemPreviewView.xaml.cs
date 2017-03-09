
using Store.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Store.Ui.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreItemPreviewView : ContentView
    {
        public StoreItemPreviewView(BookPreviewItemViewModel item)
        {
            InitializeComponent();

            BindingContext = item;

        }
        
    }
}
