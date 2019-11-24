using System.IO;
using System.Reflection;
using System.Windows;

namespace Silver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HelloClick(object sender, RoutedEventArgs e)
        {
            // Title = "Hello, World";
            // Title = System.Reflection.Assembly.GetEntryAssembly().Location;
            // Title = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            Title = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            Title += Path.DirectorySeparatorChar.ToString();
        }
    }
}
