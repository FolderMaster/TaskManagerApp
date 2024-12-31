using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// Класс пользовательского элемента страницы настроек.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{SettingsViewModel}"/>.
/// </remarks>
public partial class SettingsView : ReactiveUserControl<SettingsViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="SettingsView"/> по умолчанию.
    /// </summary>
    public SettingsView()
    {
        InitializeComponent();
    }
}