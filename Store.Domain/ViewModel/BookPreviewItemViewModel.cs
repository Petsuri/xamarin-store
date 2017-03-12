using Store.Domain;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Store.ViewModel
{
    public class BookPreviewItemViewModel : ViewModelBase
    {

        public const string ShowItemMessage = "ShowItem";
        public const string ShowOptionsMessage = "ShowOptions";

        private byte[] m_image;
        private string m_name;
        private decimal m_userScore;

        public BookPreviewItemViewModel(BookPreviewItem item, IMessageQueue messaging)
        {
            m_image = item.Image;
            m_name = item.Name;
            m_userScore = item.UserScore;

            ShowSelectedItem = new Command(() =>
            {
                messaging.Send(this, ShowItemMessage, item);
            });
            
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

        public ICommand ShowSelectedItem { get; private set; }

    }
}
