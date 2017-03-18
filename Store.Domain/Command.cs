using System;
using System.Windows.Input;

namespace Store
{
    class Command : ICommand
    {
        private Action m_execute;
        private Func<bool> m_canExecute;

        public event EventHandler CanExecuteChanged;

        public Command(Action execute) : this(execute, null)
        {
        }

        public Command(Action execute, Func<bool> canExecute)
        {
            m_execute = execute;
            m_canExecute = canExecute;
        }

        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            if (m_canExecute != null)
            {
                return m_canExecute();
            }
            else
            {
                return true;
            }

        }

        public void Execute(object parameter)
        {
            m_execute();
        }
    }

}
