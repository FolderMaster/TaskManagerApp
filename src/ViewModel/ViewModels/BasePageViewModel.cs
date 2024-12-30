namespace ViewModel.ViewModels;

/// <summary>
/// Базовый класс контроллера страницы.
/// </summary>
/// <remarks>
/// Наследует <see cref="BaseViewModel"/>.
/// </remarks>
public class BasePageViewModel : BaseViewModel
{
    /// <summary>
    /// Возращает метаданные.
    /// </summary>
    public object Metadata { get; protected set; }
}
