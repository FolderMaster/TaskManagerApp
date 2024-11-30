using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Metadata;
using ReactiveUI;
using Splat;

using ViewModel.ViewModels;

namespace View.DataTemplates
{
    public class PageDataTemplate : IRecyclingDataTemplate
    {
        [Content]
        [TemplateContent]
        public object? Content { get; set; }

        public bool Match(object? data) => data is ViewModelBase;

        public Control? Build(object? data) => Build(data, null);

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
