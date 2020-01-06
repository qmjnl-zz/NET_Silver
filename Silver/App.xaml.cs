using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace Silver
{
    public partial class App : Application
    {
        private string directory;
        private string fileName;

        public static List<CultureInfo> Languages { get; } = new List<CultureInfo>();
        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) { throw new ArgumentNullException("value"); }
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) { return; }

                ResourceDictionary dictionary = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        System.Threading.Thread.CurrentThread.CurrentUICulture = value;
                        dictionary.Source = new Uri($"Resources/lang.{value.Name}.xaml", UriKind.Relative);
                        break;
                    default:
                        System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                        dictionary.Source = new Uri("Resources/lang.en-US.xaml", UriKind.Relative);
                        break;
                }

                ResourceDictionary prevDictionary = (
                    from d in Application.Current.Resources.MergedDictionaries
                    where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                    select d).First();

                if (prevDictionary != null)
                {
                    int index = Application.Current.Resources.MergedDictionaries.IndexOf(prevDictionary);
                    Application.Current.Resources.MergedDictionaries.Remove(prevDictionary);
                    Application.Current.Resources.MergedDictionaries.Insert(index, dictionary);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                }

                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        public static AppSettings AppSettings { get; set; } = new AppSettings();

        public App()
        {
            App.LanguageChanged += App_LanguageChanged;

            Languages.Clear();
            Languages.Add(new CultureInfo("en-US"));
            Languages.Add(new CultureInfo("ru-RU"));
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "liveinmegacity",
                "Silver");
            fileName = Path.Combine(directory, "settings.json");

            try
            {
                if (File.Exists(fileName))
                {
                    string jsonString = File.ReadAllText(fileName);
                    AppSettings = JsonSerializer.Deserialize<AppSettings>(jsonString);
                }
            }
            finally
            {
                Language = new CultureInfo(AppSettings.Language ?? "en-US");
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(directory);

                var options = new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize<AppSettings>(AppSettings, options);
                File.WriteAllText(fileName, jsonString);
            }
            finally
            {
            }
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            AppSettings.Language = Language.Name;
        }
    }
}
