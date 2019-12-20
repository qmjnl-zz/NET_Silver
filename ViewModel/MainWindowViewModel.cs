using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Silver
{
    public class MainWindowViewModel
    {
        private Repository repository = new Repository();
        public ObservableCollection<Transaction> Transactions { get; set; }

        public MainWindowViewModel()
        {
            Transactions = new ObservableCollection<Transaction>(repository.GetAll<Transaction>());
            if (Transactions != null)
            {
                Transactions.CollectionChanged += TransactionsCollectionChanged;
            }
        }

        // public void Open(string fileName)
        // {
        //     if (repository.Open(fileName))
        //     {
        //         if (Transactions != null)
        //         {
        //             Transactions.CollectionChanged -= TransactionsCollectionChanged;
        //             Transactions.Clear();
        //         }

        //         Transactions = new ObservableCollection<Transaction>(repository.GetAll<Transaction>());
        //         if (Transactions != null)
        //         {
        //             Transactions.CollectionChanged += TransactionsCollectionChanged;
        //         }
        //     }
        // }

        private void TransactionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Transaction newItem in e.NewItems)
                {
                    repository.Add(newItem);
                }
            }

            if (e.OldItems != null)
            {
                foreach (Transaction oldItem in e.OldItems)
                {
                    repository.Remove(oldItem);
                }
            }
        }
    }
}
