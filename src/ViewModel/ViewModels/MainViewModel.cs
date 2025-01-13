using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;

using Model;
using Model.Interfaces;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Interfaces.ModelLearning;

namespace ViewModel.ViewModels;

/// <summary>
/// Класс главного контроллера.
/// </summary>
/// <remarks>
/// Наследует <see cref="BaseViewModel"/>.
/// </remarks>
public partial class MainViewModel : BaseViewModel
{
    /// <summary>
    /// Найстройки.
    /// </summary>
    private ISettings _settings;

    /// <summary>
    /// Сессия.
    /// </summary>
    private ISession _session;

    /// <summary>
    /// Логгирование.
    /// </summary>
    private ILogger _logger;

    /// <summary>
    /// Учителя моделей обучения на элементарных задачах.
    /// </summary>
    private IEnumerable<IModelTeacher<ITaskElement>> _modelTeachers;

    /// <summary>
    /// Логическое значение, указывающее активированн ли контроллер.
    /// </summary>
    private bool _isActivated = false;

    /// <summary>
    /// Страницы.
    /// </summary>
    [Reactive]
    private IEnumerable<BasePageViewModel> _pages;

    /// <summary>
    /// Выбранная странница.
    /// </summary>
    [Reactive]
    private BasePageViewModel? _selectedPage;

    /// <summary>
    /// Создаёт экземпляр класса <see cref="MainViewModel"/>.
    /// </summary>
    /// <param name="pages">Страницы.</param>
    /// <param name="settings">Настройки.</param>
    /// <param name="session">Сессия.</param>
    /// <param name="appLifeState">Управление жизненным циклом приложения.</param>
    /// <param name="logger">Логгирование.</param>
    /// <param name="modelTeachers">Учителя моделей обучения на элементарных задачах.</param>
    public MainViewModel(IEnumerable<BasePageViewModel> pages, ISettings settings,
        ISession session, IAppLifeState appLifeState, ILogger logger,
        IEnumerable<IModelTeacher<ITaskElement>> modelTeachers)
    {
        Pages = pages;
        _settings = settings;
        _session = session;
        _modelTeachers = modelTeachers;
        _logger = logger;

        _settings.Load();
        this.WhenActivated((Action<IDisposable> action) =>
        {
            _isActivated = true;
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
        if (!_isActivated)
        {
            return;
        }
        var taskElements = TaskHelper.GetTaskElements(_session.Tasks);
        foreach (var teacher in _modelTeachers)
        {
            try
            {
                await teacher.Train(taskElements);
            }
            catch(Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }
    }
}
