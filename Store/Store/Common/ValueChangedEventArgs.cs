using System;

namespace Store.Ui.Common
{
    public class ValueChangedEventArgs<TValue> : EventArgs
    {

        public TValue OldValue { get; private set; }
        public TValue NewValue { get; private set; }

        public ValueChangedEventArgs(TValue oldValue, TValue newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

    }
}
