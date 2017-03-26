using Store.Interface.Platform;
using Store.Model;
using System;
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
            Clear();

            TakePhoto = new Command(
                execute: async () =>
            {
                IsTakingPhoto = true;
                Photo = await camera.TakePhotoAsync();
                IsTakingPhoto = false;
            }, canExecute: () =>
            {
                return camera.IsTakePhotoSupported() && !IsTakingPhoto;
            });
            
        }


        public Review GetReview()
        {
            return new Review(null) {
                UserName = "Tuleva mobiilikehittäjä",
                Text = Text,
                Date = DateTime.Now,
                Score = Score,
                Photo = Photo
            };
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Text);
        }

        public void Clear()
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
