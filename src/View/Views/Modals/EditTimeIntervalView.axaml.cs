using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// Класс пользовательского элемента диалога изменения временного интервала.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{EditTimeIntervalViewModel}"/>.
/// </remarks>
public partial class EditTimeIntervalView : ReactiveUserControl<EditTimeIntervalViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="EditTimeIntervalView"/> по умолчанию.
    /// </summary>
    public EditTimeIntervalView()
    {
        InitializeComponent();
    }
}