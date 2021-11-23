using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace UICore
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _executeCommand = null;
        private readonly Func<object, bool> _canExecuteCommand = null;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute)
        {
            _executeCommand = execute;
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _executeCommand = execute;
            _canExecuteCommand = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteCommand != null)
            {
                return _canExecuteCommand(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            _executeCommand?.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public class DeleteCommandEx<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<T> _execute = null;
        public Func<T, bool> _canExecute = null;


        public DeleteCommandEx(Action<T> execute) : this(execute, null)
        {
        }

        public DeleteCommandEx(Action<T> execute, Func<T, bool> canExecute)
        {
            if (canExecute != null)
            {
                this._canExecute = new Func<T, bool>(canExecute);
            }
            if (execute != null)
            {
                this._execute = new Action<T>(execute);
            }
        }

        public void RaiseCanExceuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (this._canExecute == null)
            {
                return true;
            }
            return _canExecute((T)(object)(parameter));
        }

        public void Execute(object parameter)
        {
            this._execute((T)(object)(parameter));
        }
    }
}
