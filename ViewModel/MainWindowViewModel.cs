using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Silver
{
    public class MainWindowViewModel
    {
        public IList<Transaction> Transactions { get; } = new ObservableCollection<Transaction>();

        public MainWindowViewModel()
        {
            Transactions.Add(new Transaction { Amount = 120.50M, Comment = "Фрукты" });
            Transactions.Add(new Transaction { Amount = 70.45M, Comment = "Кофе" });
            Transactions.Add(new Transaction { Amount = 89.90M, Comment = "Печенье" });
        }
    }
}
