using System.Windows;

namespace Silver
{
    public class AppSettings
    {
        public double? WindowLeft { get; set; }
        public double? WindowTop { get; set; }
        public double? WindowWidth { get; set; }
        public double? WindowHeight { get; set; }

        public WindowState WindowState { get; set; } = WindowState.Normal;
    }
}
