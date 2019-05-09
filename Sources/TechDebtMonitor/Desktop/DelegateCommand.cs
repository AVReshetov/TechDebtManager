using System;
using System.Windows.Input;

using JetBrains.Annotations;

namespace Desktop
{
    public class DelegateCommand : ICommand
    {
        [NotNull] private readonly Func<bool> _canExecuteDelegate;
        [NotNull] private readonly Action _executeDelegate;

        public DelegateCommand([NotNull] Action executeDelegate, [NotNull] Func<bool> canExecuteDelegate)
        {
            _canExecuteDelegate = canExecuteDelegate;
            _executeDelegate = executeDelegate;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecuteDelegate();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            _executeDelegate.Invoke();
        }

        #endregion
    }

    public class DelegateCommand<T> : ICommand
    {
        [NotNull] private readonly Predicate<T> _canExecuteDelegate;
        [NotNull] private readonly Action<T>    _executeDelegate;

        public DelegateCommand([NotNull] Action<T>    executeDelegate,
                               [NotNull] Predicate<T> canExecuteDelegate)
        {
            _executeDelegate    = executeDelegate;
            _canExecuteDelegate = canExecuteDelegate;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecuteDelegate((T) parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            _executeDelegate.Invoke((T) parameter);
        }

        #endregion
    }
}
