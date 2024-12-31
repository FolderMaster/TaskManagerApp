using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// Класс пользовательского элемента диалога удаления задач.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{RemoveTasksViewModel}"/>.
/// </remarks>
public partial class RemoveTasksView : ReactiveUserControl<RemoveTasksViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="RemoveTasksView"/> по умолчанию.
    /// </summary>
    public RemoveTasksView()
    {
        InitializeComponent();
    }
}