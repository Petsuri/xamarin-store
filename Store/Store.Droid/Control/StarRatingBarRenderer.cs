using Android.Widget;
using Xamarin.Forms;
using Store.Ui.Control;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(StarRatingBar),
                          typeof(Store.Droid.Control.StarRatingBarRenderer))]

namespace Store.Droid.Control
{
    public class StarRatingBarRenderer :
        Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<StarRatingBar, RatingBar>,
        RatingBar.IOnRatingBarChangeListener
    {

        protected override void OnElementChanged(ElementChangedEventArgs<StarRatingBar> e)
        {
            base.OnElementChanged(e);

            if(Control == null)
            {
                SetNativeControl(new RatingBar(Context));
            }

            var isElementAttached = (e.NewElement != null);
            if (isElementAttached)
            {
                SetRatingPrecision();
                SetMaximumRating();
                SetRating();
                SetIsReadOnly();

                Control.OnRatingBarChangeListener = this;
            }
            else
            {
                Control.OnRatingBarChangeListener = null;
            }

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == StarRatingBar.RatingProperty.PropertyName)
            {
                SetRating();
            }
            else if (e.PropertyName == StarRatingBar.MaximumRatingProperty.PropertyName)
            {
                SetMaximumRating();
            }
            else if (e.PropertyName == StarRatingBar.IsReadOnlyProperty.PropertyName)
            {
                SetIsReadOnly();
            }
            else if (e.PropertyName == StarRatingBar.RatingPrecisionProperty.PropertyName)
            {
                SetRatingPrecision();
            }
            
        }


        private void SetRatingPrecision()
        {
            Control.StepSize = Element.RatingPrecision;
        }

        private void SetRating()
        {
            Control.Rating = Element.Rating;
        }
        
        private void SetMaximumRating()
        {
            Control.NumStars = Element.MaximumRating;
        }

        private void SetIsReadOnly()
        {
            Control.IsIndicator = Element.IsReadOnly;
        }

        public void OnRatingChanged(RatingBar ratingBar, float rating, bool fromUser)
        {
            ((IElementController)Element).SetValueFromRenderer(StarRatingBar.RatingProperty, rating);
        }
    }
}