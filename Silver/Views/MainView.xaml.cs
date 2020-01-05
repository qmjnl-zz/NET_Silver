using System;
using System.Windows;

namespace Silver
{
    public partial class MainView : Window
    {
        protected MainViewModel viewModel = new MainViewModel();

        public MainView()
        {
            InitializeComponent();
            DataContext = viewModel;

            Left = Math.Max(((App)Application.Current).AppSettings.WindowLeft ?? 0, 0);
            Top = Math.Max(((App)Application.Current).AppSettings.WindowTop ?? 0, 0);
            Width = Math.Max(((App)Application.Current).AppSettings.WindowWidth ?? 0, 0);
            Height = Math.Max(((App)Application.Current).AppSettings.WindowHeight ?? 0, 0);

            if (System.Enum.IsDefined(typeof(WindowState), ((App)Application.Current).AppSettings.WindowState) &&
                ((App)Application.Current).AppSettings.WindowState != WindowState.Minimized)
            {
                WindowState = ((App)Application.Current).AppSettings.WindowState;
            }
        }

        private void Window_LocationChanged(object sender, System.EventArgs e)
        {
            ((App)Application.Current).AppSettings.WindowLeft = Left;
            ((App)Application.Current).AppSettings.WindowTop = Top;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((App)Application.Current).AppSettings.WindowWidth = Width;
            ((App)Application.Current).AppSettings.WindowHeight = Height;
        }

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            ((App)Application.Current).AppSettings.WindowState = WindowState;
        }
    }
}
