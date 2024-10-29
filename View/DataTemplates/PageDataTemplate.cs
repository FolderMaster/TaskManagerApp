using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Metadata;
using System;

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
            var name = data.GetType().FullName!.
                Replace(nameof(ViewModel), nameof(View));
            var type = Type.GetType(name);
            if (type == null)
            {
                return null;
            }
            var control = (Control?)Activator.CreateInstance(type);
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
