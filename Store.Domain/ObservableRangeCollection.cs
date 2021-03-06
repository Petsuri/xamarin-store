﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Store
{
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {

        public void AddRange(IEnumerable<T> newItems)
        {
            CheckReentrancy();
            foreach(T item in newItems)
            {
                Items.Add(item);
            }

            var addedNewItems = new List<T>(newItems);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, addedNewItems));

        }

    }
}
