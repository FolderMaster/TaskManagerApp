using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// Класс пользовательского элемента страницы статистики.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{StatisticViewModel}"/>.
/// </remarks>
public partial class StatisticView : ReactiveUserControl<StatisticViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="StatisticView"/> по умолчанию.
    /// </summary>
    public StatisticView()
    {
        InitializeComponent();
    }
}