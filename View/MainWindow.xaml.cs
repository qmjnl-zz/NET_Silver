using System.IO;
using System.Reflection;
using System.Windows;
using System.Linq;
using System.Collections;
using System.Windows.Input;
using LiteDB;

namespace Silver
{
    public partial class MainWindow : Window
    {
        protected MainWindowViewModel viewModel = new MainWindowViewModel();

        private ICommand changeCommentCmd = null;
        public ICommand ChangeCommentCmd => changeCommentCmd ?? (changeCommentCmd = new ChangeCommentCommand());

        private ICommand addTransactionCmd = null;
        public ICommand AddTransactionCmd => addTransactionCmd ?? (addTransactionCmd = new AddTransactionCommand());

        private ICommand removeTransactionCmd = null;
        public ICommand RemoveTransactionCmd => removeTransactionCmd ?? (removeTransactionCmd = new RemoveTransactionCommand());

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
