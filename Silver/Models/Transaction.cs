namespace Silver
{
    public class Transaction : Entity
    {
        private decimal amount;
        private string comment;
        private int expenditureId;

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

        public int ExpenditureId
        {
            get => expenditureId;
            set
            {
                if (value == expenditureId) return;
                expenditureId = value;
                OnPropertyChanged();
            }
        }
    }
}
