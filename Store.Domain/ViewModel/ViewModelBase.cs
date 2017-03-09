using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = null)
        {

            if (!Object.Equals(currentValue, newValue))
            {
                currentValue = newValue;
                OnPropertyChanged(propertyName);

                return true;
            }
            else
            {
                return false;
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
