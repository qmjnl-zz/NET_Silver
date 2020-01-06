using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Silver
{
    public partial class MainView : Window
    {
        protected MainViewModel viewModel = new MainViewModel();

        public MainView()
        {
            InitializeComponent();
            DataContext = viewModel;

            //*****

            Left = Math.Max(App.AppSettings.WindowLeft ?? 0, 0);
            Top = Math.Max(App.AppSettings.WindowTop ?? 0, 0);
            Width = Math.Max(App.AppSettings.WindowWidth ?? 0, 0);
            Height = Math.Max(App.AppSettings.WindowHeight ?? 0, 0);

            if (System.Enum.IsDefined(typeof(WindowState), App.AppSettings.WindowState) &&
                App.AppSettings.WindowState != WindowState.Minimized)
            {
                WindowState = App.AppSettings.WindowState;
            }

            //*****

            App.LanguageChanged += LanguageChanged;
            languagesMenu.Items.Clear();
            foreach (var language in App.Languages)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Header = language.EnglishName;
                menuItem.Tag = language;
                menuItem.IsChecked = language.Equals(App.Language);
                menuItem.Click += languagesMenu_Click;
                languagesMenu.Items.Add(menuItem);
            }
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo language = App.Language;
            foreach (MenuItem menuItem in languagesMenu.Items)
            {
                CultureInfo cultureInfo = menuItem.Tag as CultureInfo;
                menuItem.IsChecked = cultureInfo != null && cultureInfo.Equals(language);
            }
        }

        private void Window_LocationChanged(object sender, System.EventArgs e)
        {
            App.AppSettings.WindowLeft = Left;
            App.AppSettings.WindowTop = Top;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            App.AppSettings.WindowWidth = Width;
            App.AppSettings.WindowHeight = Height;
        }

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            App.AppSettings.WindowState = WindowState;
        }

        private void languagesMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                CultureInfo language = menuItem.Tag as CultureInfo;
                if (language != null)
                {
                    App.Language = language;
                }
            }
        }
    }
}
