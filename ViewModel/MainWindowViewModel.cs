using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Silver
{
    public class MainWindowViewModel
    {
        public IList<Transaction> Transactions { get; } = new ObservableCollection<Transaction>();

        public MainWindowViewModel()
        {
            Transactions.Add(new Transaction { Id = 1, Amount = 120.50M, Comment = "Фрукты", IsChanged = false });
            Transactions.Add(new Transaction { Id = 2, Amount = 70.45M, Comment = "Кофе", IsChanged = false });
            Transactions.Add(new Transaction { Id = 3, Amount = 89.90M, Comment = "Печенье", IsChanged = false });
        }
    }
}
