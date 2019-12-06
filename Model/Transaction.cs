// using System.ComponentModel;
// using System.Runtime.CompilerServices;

namespace Silver
{
    public class Transaction : Entity// INotifyPropertyChanged
    {
        // private int id;
        private decimal amount;
        private string comment;
        // private bool isChanged;

        // public int Id
        // {
        //     get => id;
        //     set
        //     {
        //         if (value == id) return;
        //         id = value;
        //         OnPropertyChanged();
        //     }
        // }
        public decimal Amount
        {
            get => amount;
            set
            {
                if (value == amount) return;
                amount = value;
                OnPropertyChanged();
            }
        }
        public string Comment
        {
            get => comment;
            set
            {
                if (value == comment) return;
                comment = value;
                OnPropertyChanged();
            }
        }
        // public bool IsChanged
        // {
        //     get => isChanged;
        //     set
        //     {
        //         if (value == isChanged) return;
        //         isChanged = value;
        //         OnPropertyChanged();
        //     }
        // }


        // public event PropertyChangedEventHandler PropertyChanged;

        // protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        // {
        //     if (propertyName != nameof(IsChanged))
        //     {
        //         IsChanged = true;
        //     }
        //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        // }
    }
}
