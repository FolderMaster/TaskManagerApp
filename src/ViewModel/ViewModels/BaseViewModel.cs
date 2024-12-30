using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;

namespace ViewModel.ViewModels;

/// <summary>
/// Базовый класс для контроллеров.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveObject"/>.
/// Реализует <see cref="IActivatableViewModel"/>.
/// </remarks>
public partial class BaseViewModel : ReactiveObject, IActivatableViewModel
{
    /// <summary>
    /// Наблюдатель, который отслеживает модальные диалоги.
    /// </summary>
    /// <remarks>
    /// Возвращает <c>true</c>, если нет модальных диалогов, и <c>false</c>, если есть.
    /// </remarks>
    protected readonly IObservable<bool> _modalsObservable;

    /// <summary>
    /// Диалоги.
    /// </summary>
    private ObservableCollection<BaseViewModel> _dialogs = new();

    /// <summary>
    /// Модальные диалоги.
    /// </summary>
    private ObservableCollection<BaseViewModel> _modals = new();

    /// <summary>
    /// Возвращает диалоги.
    /// </summary>
    public ObservableCollection<BaseViewModel> Dialogs => _dialogs;

    /// <summary>
    /// Возвращает модальные диалоги.
    /// </summary>
    public ObservableCollection<BaseViewModel> Modals => _modals;

    /// <summary>
    /// Возвращает активатор.
    /// </summary>
    public ViewModelActivator Activator { get; private set; } = new ViewModelActivator();

    /// <summary>
    /// Создаёт экемпляр класса <see cref="BaseViewModel"/> по умолчанию.
    /// </summary>
    public BaseViewModel()
    {
        _modalsObservable = this.WhenAnyValue(x => x._modals).Select(x => x.Count == 0);
    }

    /// <summary>
    /// Добавляет и вызывает диалог.
    /// </summary>
    /// <typeparam name="A">>Тип данных аргументов.</typeparam>
    /// <typeparam name="R">Тип данных результата.</typeparam>
    /// <param name="dialog">Диалог.</param>
    /// <param name="args">Аргументы.</param>
    /// <returns>Возвращает задачу процесса диалога с результатом.</returns>
    public async Task<R> AddDialog<A, R>(BaseDialogViewModel<A, R> dialog, A args)
    {
        _dialogs.Add(dialog);
        var result = await dialog.Invoke(this, args);
        _dialogs.Remove(dialog);
        return result;
    }

    /// <summary>
    /// Добавляет и вызывает модальный диалог.
    /// </summary>
    /// <typeparam name="A">Тип данных аргументов.</typeparam>
    /// <typeparam name="R">Тип данных результата.</typeparam>
    /// <param name="modal">Модальное окно.</param>
    /// <param name="args">Аргументы.</param>
    /// <returns>Возвращает задачу процесса модального диалога с результатом.</returns>
    public async Task<R> AddModal<A, R>(BaseDialogViewModel<A, R> modal, A args)
    {
        _modals.Add(modal);
        var result = await AddDialog(modal, args);
        _modals.Remove(modal);
        return result;
    }
}
