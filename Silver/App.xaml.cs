using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Silver
{
    public partial class App : Application
    {
        private string directory;
        private string fileName;

        public AppSettings AppSettings { get; set; } = new AppSettings();

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
    }
}
