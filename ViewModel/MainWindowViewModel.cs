using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Silver
{
    public class MainWindowViewModel
    {
        private Repository repository = new Repository();
        public ObservableCollection<Transaction> Transactions { get; }

        public MainWindowViewModel()
        {
            Transactions = new ObservableCollection<Transaction>(repository.GetAll<Transaction>());
            Transactions.CollectionChanged += TransactionsCollectionChanged;
        }

        private void TransactionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                List<Transaction> newItems = new List<Transaction>();
                foreach (Transaction newItem in e.NewItems)
                {
                    newItems.Add(newItem);
                    repository.Add(newItem);
                }
            }

            if (e.OldItems != null)
            {
                List<Transaction> oldItems = new List<Transaction>();
                foreach (Transaction oldItem in e.OldItems)
                {
                    oldItems.Add(oldItem);
                }
            }
        }
    }
}
