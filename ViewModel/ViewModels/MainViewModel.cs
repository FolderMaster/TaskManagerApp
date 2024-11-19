using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

using ViewModel.AppStates;

namespace ViewModel.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private static readonly string _partFilePath = "TaskManager/session.json";

    private readonly string _fullFilePath;

    private readonly AppState _appState;

    [Reactive]
    private IEnumerable<PageViewModel> _pages;

    [Reactive]
    private PageViewModel? _selectedPage;

    public MainViewModel(IEnumerable<PageViewModel> pages, AppState appState)
    {
        _appState = appState;

        _fullFilePath = _appState.Services.FileService.CombinePath
            (_appState.Services.FileService.PersonalDirectoryPath, _partFilePath);

        Pages = pages;

        this.WhenAnyValue(x => x.Pages).Subscribe(s => SelectedPage = s?.FirstOrDefault());
    }

    [ReactiveCommand]
    private void Notify()
    {
        _appState.Services.NotificationManager.SendNotification("Content", "Title");
    }

    [ReactiveCommand]
    private void Save()
    {
        var data = _appState.Services.Serializer.Serialize(_appState.Session.Tasks);
        var directoryPath = _appState.Services.FileService.GetDirectoryPath(_fullFilePath);
        if (!_appState.Services.FileService.IsPathExists(directoryPath))
        {
            _appState.Services.FileService.CreateDirectory(directoryPath);
        }
        _appState.Services.FileService.Save(_fullFilePath, data);
    }

    [ReactiveCommand]
    private void Load()
    {
        if (!_appState.Services.FileService.IsPathExists(_fullFilePath))
        {
            return;
        }
        var data = _appState.Services.FileService.Load(_fullFilePath);
        _appState.Session.Tasks = _appState.Services.Serializer.
            Deserialize<IList<ITask>>(data);
    }
}
