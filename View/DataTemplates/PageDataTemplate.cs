using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;

using ViewModel.ViewModels;

namespace View.DataTemplates
{
    public class PageDataTemplate : IDataTemplate
    {
        public Control? Build(object? param)
        {
            var name = param.GetType().FullName!.
                Replace(nameof(ViewModel), nameof(View));
            var type = Type.GetType(name);
            if (type != null)
            {
                var control = (Control?)Activator.CreateInstance(type);
                if (control != null)
                {
                    control.DataContext = param;
                }
                return control;
            }
            return null;
        }

        public bool Match(object? data) => data is ViewModelBase;
    }
}
