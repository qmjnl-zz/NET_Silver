using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Silver
{
    public class MainViewModel
    {
        private Repository repository = new Repository();
        public ObservableCollection<Transaction> Transactions { get; set; }

        public MainViewModel()
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

        #region Add Transaction Command

        private RelayCommand<ObservableCollection<Transaction>> addTransactionCommand = null;
        public RelayCommand<ObservableCollection<Transaction>> AddTransactionCommand => addTransactionCommand ??
             (addTransactionCommand = new RelayCommand<ObservableCollection<Transaction>>(AddTransactionExecute, AddTransactionCanExecute));
        private void AddTransactionExecute(ObservableCollection<Transaction> transactions)
        {
            int maxId = transactions.Count > 0 ? transactions.Max(x => x.Id) : 0;
            transactions.Add(new Transaction { Id = ++maxId, Amount = 140.05M, Comment = "Свекла", IsChanged = false });
        }
        private bool AddTransactionCanExecute(ObservableCollection<Transaction> transactions)
        {
            return transactions != null;
        }

        #endregion

        #region Change Comment Command

        private RelayCommand<Transaction> changeCommentCommand = null;
        public RelayCommand<Transaction> ChangeCommentCommand => changeCommentCommand ??
             (changeCommentCommand = new RelayCommand<Transaction>(ChangeCommentExecute, ChangeCommentCanExecute));
        private void ChangeCommentExecute(Transaction transaction)
        {
            transaction.Comment = "New Commentary";
        }
        private bool ChangeCommentCanExecute(Transaction transaction)
        {
            return transaction != null;
        }

        #endregion

        #region Remove Transaction Command

        private RelayCommand<Transaction> removeTransactionCommand = null;
        public RelayCommand<Transaction> RemoveTransactionCommand => removeTransactionCommand ??
             (removeTransactionCommand = new RelayCommand<Transaction>(RemoveTransactionExecute, RemoveTransactionCanExecute));
        private void RemoveTransactionExecute(Transaction transaction)
        {
            Transactions.Remove(transaction);
        }
        private bool RemoveTransactionCanExecute(Transaction transaction)
        {
            return transaction != null;
        }

        #endregion
    }
}
