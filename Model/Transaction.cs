using System.ComponentModel.DataAnnotations;

namespace Silver
{
    public partial class Transaction : Entity
    {
        private decimal amount;
        private string comment;

        [Required]
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
    }
}
