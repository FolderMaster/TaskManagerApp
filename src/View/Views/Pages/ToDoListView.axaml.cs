using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// Класс пользовательского элемента страницы списка задач для выполнения.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{ToDoListViewModel}"/>.
/// </remarks>
public partial class ToDoListView : ReactiveUserControl<ToDoListViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="ToDoListView"/> по умолчанию.
    /// </summary>
    public ToDoListView()
    {
        InitializeComponent();
    }
}