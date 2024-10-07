using Avalonia.Markup.Xaml;
using System;

namespace View.Extensions
{
    public class EnumsExtension : MarkupExtension
    {
        private readonly Type _type;

        public EnumsExtension(Type type)
        {
            if (type == null || !type.IsEnum)
            {
                throw new ArgumentException(nameof(type));
            }
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) =>
            _type.GetEnumValues();
    }
}
