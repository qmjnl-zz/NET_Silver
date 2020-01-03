using LiteDB;
using System.ComponentModel.DataAnnotations;

namespace Silver
{
    public class Transaction : Entity
    {
        private decimal amount;
        private string comment;
        private Expenditure expenditure;

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

        [BsonRef("Expenditure")]
        public Expenditure Expenditure
        {
            get => expenditure;
            set
            {
                if (value == expenditure) return;
                expenditure = value;
                OnPropertyChanged();
            }
        }
    }
}
