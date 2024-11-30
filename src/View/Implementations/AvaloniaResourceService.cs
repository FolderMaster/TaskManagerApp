using Avalonia;

using ViewModel.Interfaces;

namespace View.Implementations
{
    public class AvaloniaResourceService : IResourceService
    {
        public object? GetResource(object key)
        {
            var application = Application.Current;
            application.Resources.TryGetResource(key,
                application.ActualThemeVariant, out var result);
            return result;
        }
    }
}
