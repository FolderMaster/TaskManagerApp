using System.Globalization;

namespace ViewModel.Implementations.AppStates.Settings
{
    public class AppConfigurationFormat
    {
        public CultureInfo? Localization { get; set; }

        public string? Theme { get; set; }

        public string? SavePath { get; set; }
    }
}
