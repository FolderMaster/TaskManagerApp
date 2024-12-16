using System.Globalization;

using TrackableFeatures;

namespace ViewModel.Technicals
{
    public class AppConfiguration : TrackableObject
    {
        private CultureInfo _actualLocalization;

        private object _actualTheme;

        private string _savePath;

        public CultureInfo ActualLocalization
        {
            get => _actualLocalization;
            set => UpdateProperty(ref _actualLocalization, value);
        }

        public IEnumerable<CultureInfo> Localizations { get; set; } =
            Enumerable.Empty<CultureInfo>();

        public object ActualTheme
        {
            get => _actualTheme;
            set => UpdateProperty(ref _actualTheme, value);
        }

        public IEnumerable<object> Themes { get; set; } = Enumerable.Empty<object>();

        public string SavePath
        {
            get => _savePath;
            set => UpdateProperty(ref _savePath, value);
        }
    }
}
