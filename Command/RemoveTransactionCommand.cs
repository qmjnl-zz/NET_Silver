using System.Collections.ObjectModel;
using System.Linq;

namespace Silver
{
    public class RemoveTransactionCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            if (parameter is object[] parameters)
            {
                if (!(parameters[0] is ObservableCollection<Transaction>))
                {
                    return false;
                }
                if (!(parameters[1] is Transaction))
                {
                    return false;
                }
            }
            return true;
        }

        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];

            ObservableCollection<Transaction> transactions = parameters[0] as ObservableCollection<Transaction>;
            Transaction transaction = parameters[1] as Transaction;

            transactions.Remove(transaction);
        }
    }
}
