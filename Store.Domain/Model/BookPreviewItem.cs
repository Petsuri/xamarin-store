using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model
{
    public class BookPreviewItem
    {
        public int Id { get; private set; }
        public byte[] ImageFile { get; set; }
        public string Name { get; set; }
        public decimal UserScore { get; set; }

        public BookPreviewItem(int id)
        {
            this.Id = id;
        }

    }
}
