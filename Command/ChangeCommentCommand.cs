using System;
using System.Windows.Input;

namespace Silver
{
    public class ChangeCommentCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return (parameter as Transaction) != null;
        }

        public void Execute(object parameter)
        {
            ((Transaction)parameter).Comment = "New Commentary";
        }
    }
}
