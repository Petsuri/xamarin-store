

using Store.Ui.Common;
using System;
using Xamarin.Forms;

namespace Store.Ui.Control
{
    public class StarRatingBar : Xamarin.Forms.View
    {

        public event EventHandler<ValueChangedEventArgs<float>> RatingChanged;


        public static readonly BindableProperty RatingProperty =
            BindableProperty.Create(
                nameof(Rating),
                typeof(float),
                typeof(StarRatingBar),
                0f,
                BindingMode.TwoWay,
                validateValue: (obj, rating) => {
                    return (float)rating <= (obj as StarRatingBar).MaximumRating;
                },
                propertyChanged: (obj, oldValue, newValue) =>
                {

                    var bar = (obj as StarRatingBar);
                    bar.RatingChanged?.Invoke(obj, new ValueChangedEventArgs<float>((float)oldValue, (float)newValue));

                });

        public static readonly BindableProperty MaximumRatingProperty =
            BindableProperty.Create(
                nameof(MaximumRating),
                typeof(int),
                typeof(StarRatingBar),
                5,
                validateValue: (obj, rating) =>
                {
                    return 0 <= (int)rating;
                });



        public float Rating
        {
            get { return (float)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        public int MaximumRating
        {
            get { return (int)GetValue(MaximumRatingProperty); }
            set { SetValue(MaximumRatingProperty, value); }
        }

    }
}
