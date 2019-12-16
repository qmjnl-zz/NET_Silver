using System;
using System.Windows.Input;

namespace Silver
{
    public class ChangeCommentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return (parameter as Transaction) != null;
        }

        public override void Execute(object parameter)
        {
            ((Transaction)parameter).Comment = "New Commentary";
        }
    }
}
