using System;

namespace Silver
{
    public class RelayCommand<T> : Command
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public RelayCommand(Action<T> execute) : this(execute, null) { }
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute((T)parameter);
        }

        public override void Execute(object parameter)
        {
            execute((T)parameter);
        }
    }
}
