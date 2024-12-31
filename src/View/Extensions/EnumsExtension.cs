using Avalonia.Markup.Xaml;
using System;

namespace View.Extensions
{
    /// <summary>
    /// Класс расширений синтаксиса разметки для перечислений.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="MarkupExtension"/>.
    /// </remarks>
    public class EnumsExtension : MarkupExtension
    {
        /// <summary>
        /// Тип перечисления.
        /// </summary>
        private readonly Type _type;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="EnumsExtension"/>.
        /// </summary>
        /// <param name="type">Тип перечисления.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывает, если тип не является перечислением или отсутствует.
        /// </exception>
        public EnumsExtension(Type type)
        {
            if (type == null || !type.IsEnum)
            {
                throw new ArgumentException(nameof(type));
            }
            _type = type;
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider) =>
            _type.GetEnumValues();
    }
}
