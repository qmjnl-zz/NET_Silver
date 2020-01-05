using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Silver
{
    public partial class DecimalBox : UserControl
    {
        public DecimalBox()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DecimalBox));

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            const string period = ".";
            TextBox textBox = (TextBox)sender;

            if (e.Key == Key.Back)
            {
                int count = textBox.Text.Count();
                int countToRemove = textBox.Text.IndexOf(period) == count - 2 ? 2 : 1;
                if (count <= 1) { countToRemove = count; }

                textBox.Text = textBox.Text.Remove(count - countToRemove, countToRemove);
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            const string period = ".";
            // const string minus = "-";
            TextBox textBox = (TextBox)sender;
            string text = string.Empty;

            // if (e.Key == Key.OemMinus || e.Key == Key.Subtract)
            // {
            //     if (string.IsNullOrEmpty(textBox.Text)) { text = minus; }
            // }
            // else
            if (e.Key == Key.Decimal || e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Oem2)
            {
                if (textBox.Text.IndexOf(period) == -1) { text = period; }
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                text = e.Key.ToString().Remove(0, 1);
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                text = e.Key.ToString().Remove(0, 6);
            }

            textBox.Text += text;
        }
    }
}
