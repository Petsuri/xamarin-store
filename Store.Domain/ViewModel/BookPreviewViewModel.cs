using Store.Interface.Domain;
using Store.Model;
using System.Windows.Input;

namespace Store.ViewModel
{
    public class BookPreviewViewModel : ViewModelBase
    {

        public const string ShowItemMessage = "ShowItem";
        public const string ShowOptionsMessage = "ShowOptions";

        private byte[] m_image;
        private string m_name;
        private decimal m_userScore;
        private bool m_isSelectable;

        public BookPreviewViewModel(BookPreview item, IMessageQueue messaging)
        {
            m_image = item.Image;
            m_name = item.Name;
            m_userScore = item.UserScore;
            m_isSelectable = true;

            ShowSelectedItem = new Command(
                execute: () =>
            {
                messaging.Send(this, ShowItemMessage, item);
            }, canExecute: () => 
            {
                return IsSelectable;
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

        public bool IsSelectable
        {
            get { return m_isSelectable; } 
            set
            {
                if (SetProperty(ref m_isSelectable, value))
                {
                    ChangeCanCommandExecute(ShowSelectedItem);
                }
            }
        }

        public ICommand ShowSelectedItem { get; private set; }

    }
}
