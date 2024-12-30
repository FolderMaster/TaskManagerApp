using ReactiveUI.SourceGenerators;
using ReactiveUI;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using DynamicData;

using Model;
using Model.Interfaces;

using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;
using ViewModel.Interfaces;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.DataManagers;

namespace ViewModel.ViewModels.Pages;

/// <summary>
/// Класс контроллера страницы календаря.
/// </summary>
/// <remarks>
/// Наследует <see cref="BasePageViewModel"/>.
/// </remarks>
public partial class TimeViewModel : BasePageViewModel
{
    /// <summary>
    /// Наблюдатель, который отслеживает возможность выполнения <see cref="Remove"/>.
    /// </summary>
    private readonly IObservable<bool> _canExecuteRemove;

    /// <summary>
    /// Наблюдатель, который отслеживает возможность выполнения <see cref="Edit"/>.
    /// </summary>
    private readonly IObservable<bool> _canExecuteEdit;

    /// <summary>
    /// Сессия.
    /// </summary>
    private ISession _session;

    /// <summary>
    /// Сервис ресурсов.
    /// </summary>
    private IResourceService _resourceService;

    /// <summary>
    /// Планировщик времени.
    /// </summary>
    private ITimeScheduler _timeScheduler;

    /// <summary>
    /// Менеджер уведомлений.
    /// </summary>
    private INotificationManager _notificationManager;

    /// <summary>
    /// Диалог добавления временного интервала.
    /// </summary>
    private BaseDialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult>
        _addTimeIntervalDialog;

    /// <summary>
    /// Диалог изменения временного интервала.
    /// </summary>
    private BaseDialogViewModel<ITimeIntervalElement, bool> _editTimeIntervalDialog;

    /// <summary>
    /// Фабрика, создающая элементарный временной интервал.
    /// </summary>
    private IFactory<ITimeIntervalElement> _timeIntervalElementFactory;

    /// <summary>
    /// Заместитель элементарный временной интервал для редактирования.
    /// </summary>
    private ITimeIntervalElementsEditorProxy _timeIntervalElementsEditorProxy;

    /// <summary>
    /// Словарь планировщика задач.
    /// </summary>
    private Dictionary<DateTime, IEnumerable<ITaskElement>> _tasksSchedulerDictionary;

    /// <summary>
    /// Список интервалов календаря.
    /// </summary>
    [Reactive]
    private IList<CalendarInterval> _calendarIntervals =
        new ObservableCollection<CalendarInterval>();

    /// <summary>
    /// Выбранный интервал календаря.
    /// </summary>
    [Reactive]
    private CalendarInterval? _selectedCalendarInterval;

    /// <summary>
    /// Текущая неделя.
    /// </summary>
    [Reactive]
    private DateTime _currentWeek = DateTime.Now;

    /// <summary>
    /// Создаёт экземпляр класса <see cref="TimeViewModel"/>.
    /// </summary>
    /// <param name="session">Сессия.</param>
    /// <param name="resourceService">Сервис ресурсов.</param>
    /// <param name="timeScheduler">Планировщик времени.</param>
    /// <param name="notificationManager">Менеджер уведомлений.</param>
    /// <param name="addTimeIntervalDialog">Диалог добавления временного интервала.</param>
    /// <param name="editTimeIntervalDialog">Диалог изменения временного интервала.</param>
    /// <param name="timeIntervalElementFactory">
    /// Фабрика, создающая элементарный временной интервал.
    /// </param>
    /// <param name="timeIntervalElementsEditorProxy">
    /// Заместитель элементарный временной интервал для редактирования.
    /// </param>
    public TimeViewModel(ISession session, IResourceService resourceService,
        ITimeScheduler timeScheduler, INotificationManager notificationManager,
        BaseDialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult>
            addTimeIntervalDialog,
        BaseDialogViewModel<ITimeIntervalElement, bool> editTimeIntervalDialog,
        IFactory<ITimeIntervalElement> timeIntervalElementFactory,
        ITimeIntervalElementsEditorProxy timeIntervalElementsEditorProxy)
    {
        _session = session;
        _resourceService = resourceService;
        _timeScheduler = timeScheduler;
        _notificationManager = notificationManager;
        _addTimeIntervalDialog = addTimeIntervalDialog;
        _editTimeIntervalDialog = editTimeIntervalDialog;
        _timeIntervalElementFactory = timeIntervalElementFactory;
        _timeIntervalElementsEditorProxy = timeIntervalElementsEditorProxy;

        _canExecuteRemove = this.WhenAnyValue(c => c.SelectedCalendarInterval).
            Select(c => c != null).CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
        _canExecuteEdit = this.WhenAnyValue(c => c.SelectedCalendarInterval).
            Select(c => c != null).CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);

        Metadata = resourceService.GetResource("TimePageMetadata");
        _session.ItemsUpdated += Session_ItemsUpdated;
        _timeScheduler.TimepointReached += TimeScheduler_TimepointReached;
    }

    /// <summary>
    /// Обновляет данные.
    /// </summary>
    [ReactiveCommand]
    private void Update()
    {
        if (_session.Tasks == null)
        {
            return;
        }
        CalendarIntervals.Clear();
        var tasks = TaskHelper.GetTaskElements(_session.Tasks);
        foreach (var task in tasks)
        {
            foreach(var timeInterval in task.TimeIntervals)
            {
                CalendarIntervals.Add(new CalendarInterval(timeInterval, task));
            }
        }
        _tasksSchedulerDictionary = tasks.SelectMany(t => t.TimeIntervals).GroupBy(i => i.Start).
            Where(g => g.Key > DateTime.Now).ToDictionary(g => g.Key,
            g => tasks.Where(task => task.TimeIntervals.Any(i => i.Start == g.Key)));
        _timeScheduler.Timepoints.Clear();
        _timeScheduler.Timepoints.AddRange(_tasksSchedulerDictionary.Keys);
    }

    /// <summary>
    /// Переходит на следующую неделю.
    /// </summary>
    [ReactiveCommand]
    private void GoToNext() => CurrentWeek = CurrentWeek.AddDays(7);

    /// <summary>
    /// Переходит на текущую неделю.
    /// </summary>
    [ReactiveCommand]
    private void GoToNow() => CurrentWeek = DateTime.Now;

    /// <summary>
    /// Переходит на предыдущую неделю.
    /// </summary>
    [ReactiveCommand]
    private void GoToPrevious() => CurrentWeek = CurrentWeek.AddDays(-7);

    /// <summary>
    /// Добавляет временной интервал.
    /// </summary>
    /// <returns>Возвращет задачу процесса добавления.</returns>
    [ReactiveCommand(CanExecute = nameof(_modalsObservable))]
    private async Task Add()
    {
        var timeIntervalElement = _timeIntervalElementFactory.Create();
        var result = await AddModal(_addTimeIntervalDialog,
            new TimeIntervalViewModelArgs(_session.Tasks, _session.Tasks, timeIntervalElement));
        if (result != null)
        {
            _session.AddTimeInterval(result.TimeIntervalElement, result.TaskElement);
        }
    }

    /// <summary>
    /// Удаляет выбранный временной интервал.
    /// </summary>
    [ReactiveCommand(CanExecute = nameof(_canExecuteRemove))]
    private void Remove()
    {
        _session.RemoveTimeInterval(SelectedCalendarInterval.TimeInterval,
            SelectedCalendarInterval.TaskElement);
    }

    /// <summary>
    /// Изменяет выбранный временной интервал.
    /// </summary>
    /// <returns>Возвращет задачу процесса изменения.</returns>
    [ReactiveCommand(CanExecute = nameof(_canExecuteEdit))]
    private async Task Edit()
    {
        var timeInterval = SelectedCalendarInterval.TimeInterval;
        var editorService = _timeIntervalElementsEditorProxy;
        editorService.Target = timeInterval;
        var result = await AddModal(_editTimeIntervalDialog, editorService);
        if (result)
        {
            editorService.ApplyChanges();
            _session.EditTimeInterval(timeInterval);
        }
    }

    private void Session_ItemsUpdated(object? sender, ItemsUpdatedEventArgs e) => Update();

    private void TimeScheduler_TimepointReached(object? sender, DateTime e)
    {
        var titleResource = _resourceService.GetResource("TimeSchedulerNotificationTitle");
        var contentResource = _resourceService.GetResource("TimeSchedulerNotificationContent");
        
        var content = $"{contentResource}\n";
        foreach (var task in _tasksSchedulerDictionary[e])
        {
            content += $"- {task.Metadata}\n";
        }
        _notificationManager.SendNotification(content, $"{titleResource}");
    }
}
