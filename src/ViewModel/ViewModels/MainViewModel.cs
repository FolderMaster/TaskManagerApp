using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;

using Model.Interfaces;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Interfaces.ModelLearning;
using Model;
using System.Diagnostics;

namespace ViewModel.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private ISettings _settings;

    private ISession _session;

    private IEnumerable<IModelTeacher<ITaskElement>> _modelTeachers;

    [Reactive]
    private IEnumerable<PageViewModel> _pages;

    [Reactive]
    private PageViewModel? _selectedPage;

    public MainViewModel(IEnumerable<PageViewModel> pages, ISettings settings,
        ISession session, IAppLifeState appLifeState,
        IEnumerable<IModelTeacher<ITaskElement>> modelTeachers)
    {
        Pages = pages;
        _settings = settings;
        _session = session;
        _modelTeachers = modelTeachers;

        _settings.Load();
        this.WhenActivated((Action<IDisposable> action) =>
        {
            _session.Load();
        });

        this.WhenAnyValue(x => x.Pages).Subscribe(s => SelectedPage = s?.FirstOrDefault());
        appLifeState.AppDeactivated += AppLifeState_AppDeactivated;
        _session.ItemsUpdated += Session_ItemsUpdated;
    }

    private void AppLifeState_AppDeactivated(object? sender, EventArgs e)
    {
        _settings.Save();
        _session.Save();
    }

    private async void Session_ItemsUpdated(object? sender, ItemsUpdatedEventArgs e)
    {
        var taskElements = TaskHelper.GetTaskElements(_session.Tasks);
        foreach (var teacher in _modelTeachers)
        {
            try
            {
                await teacher.Train(taskElements);
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}
