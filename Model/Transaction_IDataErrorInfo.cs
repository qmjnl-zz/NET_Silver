using System.ComponentModel;

namespace Silver
{
    public partial class Transaction : IDataErrorInfo
    {
        private string error = string.Empty;
        public string Error => error;

        public string this[string columnName]
        {
            get
            {
                bool hasErrors = false;
                switch (columnName)
                {
                    case nameof(Amount):
                        if (Amount <= 0)
                        {
                            AddError(nameof(Amount), "Amount must be a positive number");
                            hasErrors = true;
                        }
                        if (!hasErrors)
                        {
                            ClearErrors(nameof(Amount));
                        }
                        break;
                    default:
                        break;
                }
                return string.Empty;
            }
        }
    }
}
