using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

using ViewModel.AppState;

namespace ViewModel.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private static readonly string _partFilePath = "TaskManager/session.json";

    private readonly string _fullFilePath;

    private readonly AppStateManager _appStateManager;

    [Reactive]
    private IEnumerable<PageViewModel> _pages;

    [Reactive]
    private PageViewModel? _selectedPage;

    public MainViewModel(IEnumerable<PageViewModel> pages, AppStateManager appStateManager)
    {
        _appStateManager = appStateManager;

        _fullFilePath = _appStateManager.Services.FileService.CombinePath
            (_appStateManager.Services.FileService.PersonalDirectoryPath, _partFilePath);

        Pages = pages;

        this.WhenAnyValue(x => x.Pages).Subscribe(s => SelectedPage = s?.FirstOrDefault());
    }

    [ReactiveCommand]
    private void Notify()
    {
        _appStateManager.Services.NotificationManager.SendNotification("Content", "Title");
    }

    [ReactiveCommand]
    private void Save()
    {
        var data = _appStateManager.Services.Serializer.Serialize(_appStateManager.Session.Tasks);
        var directoryPath = _appStateManager.Services.FileService.GetDirectoryPath(_fullFilePath);
        if (!_appStateManager.Services.FileService.IsPathExists(directoryPath))
        {
            _appStateManager.Services.FileService.CreateDirectory(directoryPath);
        }
        _appStateManager.Services.FileService.Save(_fullFilePath, data);
    }

    [ReactiveCommand]
    private void Load()
    {
        if (!_appStateManager.Services.FileService.IsPathExists(_fullFilePath))
        {
            return;
        }
        var data = _appStateManager.Services.FileService.Load(_fullFilePath);
        _appStateManager.Session.Tasks = _appStateManager.Services.Serializer.
            Deserialize<IList<ITask>>(data);
    }
}
