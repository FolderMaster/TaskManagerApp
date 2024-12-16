using ReactiveUI;
using ReactiveUI.SourceGenerators;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Settings;

namespace ViewModel.ViewModels.Pages
{
    public partial class SettingsViewModel : PageViewModel
    {
        private ISettings _settings;

        [Reactive]
        public object _configuration;

        public SettingsViewModel(ISettings settings, IResourceService resourceService)
        {
            _settings = settings;
            Configuration = _settings.Configuration;

            this.WhenAnyValue(x => x._settings.Configuration).Subscribe
                (c => Configuration = c);

            Metadata = resourceService.GetResource("SettingsPageMetadata");
        }
    }
}
