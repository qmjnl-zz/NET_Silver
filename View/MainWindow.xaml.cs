using System.IO;
using System.Reflection;
using System.Windows;
using System.Linq;
using System.Collections;
using System.Windows.Input;
using LiteDB;

namespace Silver
{
    /// <summary>
    /// Create your POCO class entity
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Phones { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

        private void HelloClick(object sender, RoutedEventArgs e)
        {
            string filename = Assembly.GetExecutingAssembly().CodeBase;
            filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            filename += Path.DirectorySeparatorChar;
            filename += "data.silver";

            filename = "data.silver";

            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(filename))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Customer>("customers");

                // Create your new customer instance
                var customer = new Customer
                {
                    Name = "John Doe",
                    Phones = new string[] { "8000-0000", "9000-0000" },
                    IsActive = true
                };

                // Insert new customer document (Id will be auto-incremented)
                col.Insert(customer);

                // Update a document inside a collection
                customer.Name = "Joana Doe";
                col.Update(customer);

                // Index document using document Name property
                col.EnsureIndex(x => x.Name);

                // Use LINQ to query documents
                var results = col.Find(x => x.Name.StartsWith("Jo"));

                // Let's create an index in phone numbers (using expression). It's a multikey index
                col.EnsureIndex(x => x.Phones, "$.Phones[*]");

                // and now we can query phones
                // var r = col.FindOne(x => x.Phones.Contains("8888-5555"));
                var r = col.FindOne(x => (x.Phones as IList).Contains("8888-5555"));
            }
        }
    }
}
