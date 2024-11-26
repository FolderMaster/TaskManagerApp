using System.Globalization;

namespace ViewModel.Interfaces
{
    public interface ILocalizationManager
    {
        public IEnumerable<CultureInfo> Localizations { get; }

        public CultureInfo ActualLocalization { get; set; }
    }
}
