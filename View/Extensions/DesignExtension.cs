using Autofac;
using Avalonia.Markup.Xaml;
using System;
using View.Technilcals;
using ViewModel.ViewModels;

namespace View.Extensions
{
    public class DesignExtension : MarkupExtension
    {
        private readonly Type _type;

        private static readonly IContainer _container = ContainerHelper.GetMockContainer();

        public DesignExtension(Type type)
        {
            if (type == null || !type.IsAssignableTo(typeof(ViewModelBase)) || type.IsAbstract)
            {
                throw new ArgumentException(nameof(type));
            }
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) =>
            _container.Resolve(_type);
    }
}
