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
    public class WriteReviewViewModel : ViewModelBase
    {
        
        private int m_reviewScore;
        private string m_reviewText;
        private byte[] m_reviewPhoto;
        private bool m_isTakingPhoto;

        public WriteReviewViewModel(ICamera camera)
        {
            clear();

            TakePhoto = new Command(
                execute: async () =>
            {
                IsTakingPhoto = true;
                Photo = await camera.takePhotoAsync();
                IsTakingPhoto = false;
            }, canExecute: () =>
            {
                return camera.isTakePhotoSupported() && !IsTakingPhoto;
            });
            
        }


        public Review getReview()
        {
            return new Review(null) {
                UserName = "Tuleva mobiilikehittäjä",
                Text = Text,
                Date = DateTime.Now,
                Score = Score,
                Photo = Photo
            };
        }

        public bool isValid()
        {
            return !string.IsNullOrEmpty(Text);
        }

        public void clear()
        {
            Score = 1;
            Text = "";
            Photo = null;
            m_isTakingPhoto = false;
        }

        public ICommand TakePhoto { get; private set; }

        public int Score
        {
            get { return m_reviewScore; }
            set { SetProperty(ref m_reviewScore, value); }
        }

        public string Text
        {
            get { return m_reviewText; }
            set { SetProperty(ref m_reviewText, value); }
        }
       
        public byte[] Photo
        {
            get { return m_reviewPhoto; }
            set { SetProperty(ref m_reviewPhoto, value); }
        }

        public bool IsTakingPhoto
        {
            get { return m_isTakingPhoto; }
            set
            {
                if (SetProperty(ref m_isTakingPhoto, value))
                {
                    (TakePhoto as Command).ChangeCanExecute();
                }
            }
        }

    }
}
