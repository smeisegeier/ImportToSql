using System;
using System.Windows.Input;

namespace Rki.ImportToSql
{
    /// <summary>
    /// Implements ICommand to bind commands to wpf.
    /// can be called w/ |object| as type, when there is none to be passed
    /// https://www.c-sharpcorner.com/UploadFile/20c06b/icommand-and-relaycommand-in-wpf/
    /// https://stackoverflow.com/questions/43372669/wpf-mvvm-relaycommand-action-canexecute-parameter
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<T> execute) : this(execute, null) { }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }

    //public class RelayCommand : ICommand
    //{
    //    private Action<object> execute;
    //    private Func<object, bool> canExecute;

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add { CommandManager.RequerySuggested += value; }
    //        remove { CommandManager.RequerySuggested -= value; }
    //    }

    //    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    //    {
    //        this.execute = execute;
    //        this.canExecute = canExecute;
    //    }

    //    public RelayCommand(Action<object> execute) : this(execute, null) { }

    //    public bool CanExecute(object parameter)
    //    {
    //        return this.canExecute == null || this.canExecute(parameter);
    //    }

    //    public void Execute(object parameter)
    //    {
    //        this.execute(parameter);
    //    }
    //}
}
