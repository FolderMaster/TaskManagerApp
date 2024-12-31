using Autofac;
using Avalonia.Markup.Xaml;
using System;

using ViewModel.ViewModels;

using View.Technilcals;

namespace View.Extensions
{
    /// <summary>
    /// Класс расширений синтаксиса разметки для дизайнера,
    /// который использует контейнер зависимости.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="MarkupExtension"/>.
    /// </remarks>
    public class DesignExtension : MarkupExtension
    {
        /// <summary>
        /// Тип перечисления.
        /// </summary>
        private readonly Type _type;

        /// <summary>
        /// Контейнер зависимости.
        /// </summary>
        private static readonly IContainer _container = ViewContainerHelper.GetMockContainer();

        /// <summary>
        /// Создаёт экземпляр класса <see cref="EnumsExtension"/>.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывает, если тип отсутствует.
        /// </exception>
        public DesignExtension(Type type)
        {
            if (type == null || !type.IsAssignableTo(typeof(BaseViewModel)) || type.IsAbstract)
            {
                throw new ArgumentException(nameof(type));
            }
            _type = type;
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider) =>
            _container.Resolve(_type);
    }
}
