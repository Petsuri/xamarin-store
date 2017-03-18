using System;

namespace Store.Model
{
    public class Book : BookPreview
    {

        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long PurchasedCount { get; set; }
        public long ReviewerCount { get; set; }
        public DateTime PublishedDate { get; set; }
        
        public Book(int id) : base(id)
        {
        }
        
    }
}
