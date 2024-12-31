using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// Класс пользовательского элемента диалога добавления задачи.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{AddTaskViewModel}"/>.
/// </remarks>
public partial class AddTaskView : ReactiveUserControl<AddTaskViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="AddTaskView"/> по умолчанию.
    /// </summary>
    public AddTaskView()
    {
        InitializeComponent();
    }
}