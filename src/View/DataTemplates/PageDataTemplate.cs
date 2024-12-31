using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Metadata;
using ReactiveUI;
using Splat;

using ViewModel.ViewModels;

namespace View.DataTemplates
{
    /// <summary>
    /// Класс шаблона данных для страниц.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IRecyclingDataTemplate"/>.
    /// </remarks>
    public class PageDataTemplate : IRecyclingDataTemplate
    {
        /// <summary>
        /// Возвращает и задаёт контент.
        /// </summary>
        [Content]
        [TemplateContent]
        public object? Content { get; set; }

        /// <inheritdoc/>
        public bool Match(object? data) => data is BaseViewModel;

        /// <inheritdoc/>
        public Control? Build(object? data) => Build(data, null);

        /// <inheritdoc/>
        public Control? Build(object? data, Control? existing)
        {
            var type = typeof(IViewFor<>).MakeGenericType(data.GetType());
            var control = (Control?)Locator.Current.GetService(type);
            var result = existing ?? TemplateContent.Load(Content)?.Result;
            if (result != null)
            {
                result.DataContext = control;
            }
            if (control != null)
            {
                control.DataContext = data;
            }
            return result;
        }
    }
}
