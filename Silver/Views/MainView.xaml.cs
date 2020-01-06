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

            Left = Math.Max(((App)Application.Current).AppSettings.WindowLeft ?? 0, 0);
            Top = Math.Max(((App)Application.Current).AppSettings.WindowTop ?? 0, 0);
            Width = Math.Max(((App)Application.Current).AppSettings.WindowWidth ?? 0, 0);
            Height = Math.Max(((App)Application.Current).AppSettings.WindowHeight ?? 0, 0);

            if (System.Enum.IsDefined(typeof(WindowState), ((App)Application.Current).AppSettings.WindowState) &&
                ((App)Application.Current).AppSettings.WindowState != WindowState.Minimized)
            {
                WindowState = ((App)Application.Current).AppSettings.WindowState;
            }

            //*****

            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;

            //Заполняем меню смены языка:
            menuLanguage.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.EnglishName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);
            }
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in menuLanguage.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
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
