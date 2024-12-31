using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// Класс пользовательского элемента диалога добавления временного интервала.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{AddTimeIntervalViewModel}"/>.
/// </remarks>
public partial class AddTimeIntervalView : ReactiveUserControl<AddTimeIntervalViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="AddTimeIntervalView"/> по умолчанию.
    /// </summary>
    public AddTimeIntervalView()
    {
        InitializeComponent();
    }
}