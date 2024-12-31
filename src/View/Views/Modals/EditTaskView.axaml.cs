using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// Класс пользовательского элемента диалога изменения задачи.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{EditTaskViewModel}"/>.
/// </remarks>
public partial class EditTaskView : ReactiveUserControl<EditTaskViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="EditTaskView"/> по умолчанию.
    /// </summary>
    public EditTaskView()
    {
        InitializeComponent();
    }
}