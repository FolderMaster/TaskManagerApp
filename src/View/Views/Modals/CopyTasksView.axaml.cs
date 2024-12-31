using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// Класс пользовательского элемента диалога копирования задач.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{CopyTasksViewModel}"/>.
/// </remarks>
public partial class CopyTasksView : ReactiveUserControl<CopyTasksViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="CopyTasksView"/> по умолчанию.
    /// </summary>
    public CopyTasksView()
    {
        InitializeComponent();
    }
}