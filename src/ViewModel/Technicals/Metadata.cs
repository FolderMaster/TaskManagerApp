using TrackableFeatures;

namespace ViewModel.Technicals
{
    /// <summary>
    /// Класс метаданных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="ICloneable"/>.
    /// </remarks>
    public class Metadata : TrackableObject, ICloneable
    {
        /// <summary>
        /// Название.
        /// </summary>
        private string _title;

        /// <summary>
        /// Описание.
        /// </summary>
        private string? _description;

        /// <summary>
        /// Категория.
        /// </summary>
        private string? _category;

        /// <summary>
        /// Теги.
        /// </summary>
        private IEnumerable<string> _tags = Enumerable.Empty<string>();

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Title
        {
            get => _title;
            set => UpdateProperty(ref _title, value);
        }

        /// <summary>
        /// Возвращает и задаёт описание.
        /// </summary>
        public string? Description
        {
            get => _description;
            set => UpdateProperty(ref _description, value);
        }

        /// <summary>
        /// Возвращает и задаёт категорию.
        /// </summary>
        public string? Category
        {
            get => _category;
            set => UpdateProperty(ref _category, value);
        }

        /// <summary>
        /// Возвращает и задаёт теги.
        /// </summary>
        public IEnumerable<string> Tags
        {
            get => _tags;
            set => UpdateProperty(ref _tags, value.Distinct().ToList());
        }

        /// <inheritdoc/>
        public object Clone() => new Metadata()
        {
            Title = Title,
            Description = Description,
            Category = Category,
            Tags = Tags.ToList()
        };

        /// <inheritdoc/>
        public override string ToString() => $"{Title}";
    }
}
