using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// Класс пользовательского элемента страницы изменения задач.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{EditorViewModel}"/>.
/// </remarks>
public partial class EditorView : ReactiveUserControl<EditorViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="EditorView"/> по умолчанию.
    /// </summary>
    public EditorView()
    {
        InitializeComponent();
    }       
}