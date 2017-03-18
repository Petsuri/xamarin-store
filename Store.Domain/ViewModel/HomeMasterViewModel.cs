using Store.Model;
using System.Collections.Generic;

namespace Store.ViewModel
{
    public class HomeMasterViewModel : ViewModelBase
    {

        public HomeMasterViewModel()
        {
            Items = new ObservableRangeCollection<MasterPageItem>();
            Items.AddRange(MasterPageItems());
        }

        private IEnumerable<MasterPageItem> MasterPageItems()
        {
            MasterPageItem[] pageItems =
            {
                new MasterPageItem() { Title = "Omat kirjat", IconSource = "ic_collections_bookmark_black_18dp.png" }

            };

            return pageItems;
            
        }

        public ObservableRangeCollection<MasterPageItem> Items { get; private set; }

    }
}
