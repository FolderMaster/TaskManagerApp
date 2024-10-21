using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using ViewModel.ViewModels.Pages;

namespace ViewModel.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly INotificationManager _notificationManager;

    [Reactive]
    private IEnumerable<PageViewModel> _pages;

    [Reactive]
    private PageViewModel? _selectedPage;

    public MainViewModel(IEnumerable<PageViewModel> pages,
        INotificationManager notificationManager)
    {
        _notificationManager = notificationManager;
        Pages = pages;

        this.WhenAnyValue(x => x.Pages).Subscribe
            (p => SelectedPage = p != null ? p.FirstOrDefault() : null);
    }

    [ReactiveCommand]
    private void Notify()
    {
        _notificationManager.SendNotification("Content", "Title");
    }
}
