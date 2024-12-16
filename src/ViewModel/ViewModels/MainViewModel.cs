using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;

namespace ViewModel.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private ISettings _settings;

    private ISession _session;

    [Reactive]
    private IEnumerable<PageViewModel> _pages;

    [Reactive]
    private PageViewModel? _selectedPage;

    public MainViewModel(IEnumerable<PageViewModel> pages, ISettings settings,
        ISession session, IAppLifeState appLifeState)
    {
        Pages = pages;
        _settings = settings;
        _session = session;

        _settings.Load();
        this.WhenActivated((Action<IDisposable> action) =>
        {
            _session.Load();
        });

        this.WhenAnyValue(x => x.Pages).Subscribe(s => SelectedPage = s?.FirstOrDefault());
        appLifeState.AppDeactivated += AppLifeState_AppDeactivated;
    }

    private void AppLifeState_AppDeactivated(object? sender, EventArgs e)
    {
        _settings.Save();
        _session.Save();
    }
}
