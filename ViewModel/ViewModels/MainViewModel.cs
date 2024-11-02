using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model;
using ViewModel.Technicals;

namespace ViewModel.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private static readonly string _partFilePath = "TaskManager/session.json";

    private readonly string _fullFilePath;

    private readonly Session _session;

    private readonly INotificationManager _notificationManager;

    private readonly IFileService _fileService;

    private readonly ISerializer _serializer;

    [Reactive]
    private IEnumerable<PageViewModel> _pages;

    [Reactive]
    private PageViewModel? _selectedPage;

    public MainViewModel(IEnumerable<PageViewModel> pages, Session session,
        INotificationManager notificationManager, IFileService fileService, ISerializer serializer)
    {
        _notificationManager = notificationManager;
        _fileService = fileService;
        _serializer = serializer;
        _session = session;

        _fullFilePath = _fileService.CombinePath(_fileService.PersonalDirectoryPath, _partFilePath);

        Pages = pages;

        this.WhenAnyValue(x => x.Pages).Subscribe(s => SelectedPage = s?.FirstOrDefault());
    }

    [ReactiveCommand]
    private void Notify()
    {
        _notificationManager.SendNotification("Content", "Title");
    }

    [ReactiveCommand]
    private void Save()
    {
        var data = _serializer.Serialize(_session.Tasks);
        var directoryPath = _fileService.GetDirectoryPath(_fullFilePath);
        if (!_fileService.IsPathExists(directoryPath))
        {
            _fileService.CreateDirectory(directoryPath);
        }
        _fileService.Save(_fullFilePath, data);
    }

    [ReactiveCommand]
    private void Load()
    {
        if (!_fileService.IsPathExists(_fullFilePath))
        {
            return;
        }
        var data = _fileService.Load(_fullFilePath);
        _session.Tasks = _serializer.Deserialize<IList<ITask>>(data);
    }
}
