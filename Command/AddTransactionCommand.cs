using System.Collections.ObjectModel;
using System.Linq;

namespace Silver
{
    public class AddTransactionCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter != null && parameter is ObservableCollection<Transaction>;
        }

        public override void Execute(object parameter)
        {
            if (parameter is ObservableCollection<Transaction> transactions)
            {
                int maxId = transactions?.Max(x => x.Id) ?? 0;
                transactions?.Add(new Transaction { Id = ++maxId, Amount = 150.00M, Comment = "Картофель", IsChanged = false });
            }
        }
    }
}
