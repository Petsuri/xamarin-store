using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModel
{
    public class HomeMasterViewModel : ViewModelBase
    {

        public HomeMasterViewModel()
        {
            Items = new ObservableRangeCollection<MasterPageItem>();
            Items.AddRange(masterPageItems());
        }

        private IEnumerable<MasterPageItem> masterPageItems()
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
