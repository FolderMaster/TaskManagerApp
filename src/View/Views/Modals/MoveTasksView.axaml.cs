using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// Класс пользовательского элемента диалога перемещения задач.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{MoveTasksViewModel}"/>.
/// </remarks>
public partial class MoveTasksView : ReactiveUserControl<MoveTasksViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="MoveTasksView"/> по умолчанию.
    /// </summary>
    public MoveTasksView()
    {
        InitializeComponent();
    }
}