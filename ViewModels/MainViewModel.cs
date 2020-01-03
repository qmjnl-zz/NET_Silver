using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

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

        private void TransactionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Transaction newItem in e.NewItems)
                {
                    // repository.Upsert(newItem);
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
            // transactions.Add(new Transaction { Id = ++maxId, Amount = 140.05M, Comment = "Свекла", IsChanged = false });
            transactions.Add(new Transaction { Id = ++maxId});
        }
        private bool AddTransactionCanExecute(ObservableCollection<Transaction> transactions)
        {
            return transactions != null;
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

        #region Undo Command

        private RelayCommand<Transaction> undoCommand = null;
        public RelayCommand<Transaction> UndoCommand => undoCommand ??
             (undoCommand = new RelayCommand<Transaction>(UndoExecute, UndoCanExecute));
        private void UndoExecute(Transaction transaction)
        {
            if (transaction.Id > 0)
            {
                var entity = repository.Get(transaction);
                PropertyInfo[] propertyInfo;
                propertyInfo = Type.GetType(typeof(Transaction).ToString()).GetProperties();
                for (int i = 0; i < propertyInfo.Length; i++)
                {
                    propertyInfo[i].SetValue(transaction, propertyInfo[i].GetValue(entity));
                }
                transaction.IsChanged = false;
            }
            else
            {
                Transactions.Remove(transaction);
            }
        }
        private bool UndoCanExecute(Transaction transaction)
        {
            return transaction != null && transaction.IsChanged == true;
        }

        #endregion

        #region Save Command

        private RelayCommand<Transaction> saveCommand = null;
        public RelayCommand<Transaction> SaveCommand => saveCommand ??
             (saveCommand = new RelayCommand<Transaction>(SaveExecute, SaveCanExecute));
        private void SaveExecute(Transaction transaction)
        {
            // transaction.IsChanged = false;
            repository.Upsert(transaction);
        }
        private bool SaveCanExecute(Transaction transaction)
        {
            return transaction != null && transaction.IsChanged == true;
        }

        #endregion
    }
}
