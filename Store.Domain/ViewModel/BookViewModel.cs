using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        private byte[] m_image;
        private string m_name;
        private decimal m_userScore;

        private string m_author;
        private string m_description;
        private decimal m_price;
        private long m_purchasedCount;
        private long m_reviewerCount;
        private DateTime m_publishedDate;

        public BookViewModel(Book b)
        {
            m_image = b.Image;
            m_name = b.Name;
            m_userScore = b.UserScore;
            m_author = b.Author;
            m_description = b.Description;
            m_price = b.Price;
            m_purchasedCount = b.PurchasedCount;
            m_reviewerCount = b.ReviewerCount;
            m_publishedDate = b.PublishedDate;
        }

        public byte[] Image
        {
            get { return m_image; }
            set { SetProperty(ref m_image, value); }
        }

        public string Name
        {
            get { return m_name; }
            set { SetProperty(ref m_name, value); }
        }

        public decimal UserScore
        {
            get { return m_userScore; }
            set { SetProperty(ref m_userScore, value); }
        }

        public string Author
        {
            get { return m_author; }
            set { SetProperty(ref m_author, value); }
        }

        public decimal Price
        {
            get { return m_price; }
            set { SetProperty(ref m_price, value); }
        }

        public long PurchasedCount
        {
            get { return m_purchasedCount; }
            set { SetProperty(ref m_purchasedCount, value); }
        }

        public long ReviewerCount
        {
            get { return m_reviewerCount; }
            set { SetProperty(ref m_reviewerCount, value); }
        }

        public DateTime PublishedDate
        {
            get { return m_publishedDate; }
            set { SetProperty(ref m_publishedDate, value); }
        }
        
    }
}
