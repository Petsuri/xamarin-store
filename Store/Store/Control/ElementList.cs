using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace Store.Ui.Control
{
    class ElementList<TView, TItem> : StackLayout where TView : Xamarin.Forms.View, new()
    {

        public static BindableProperty ElementsSourceProperty = BindableProperty.Create(
            nameof(ElementsSource),
            typeof(ObservableCollection<TItem>),
            typeof(ElementList<TView, TItem>),
            null,
            propertyChanged: (sender, oldValue, newValue) =>
            {

                var elementList = (sender as ElementList<TView, TItem>);
                if (elementList != null)
                {
                    elementList.ElementsSource.CollectionChanged += elementList.HandleElementListChange;
                }

            });

        public ObservableCollection<TItem> ElementsSource
        {
            get { return (ObservableCollection<TItem>)GetValue(ElementsSourceProperty); }
            set { SetValue(ElementsSourceProperty, value); }
        }
        
        private void HandleElementListChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

                foreach(var addedItem in e.NewItems)
                {
                    var view = new TView();
                    view.BindingContext = addedItem;
                    Children.Add(view);
                }


            }


        }
    }
}
