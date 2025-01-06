using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    /// <summary>
    /// Класс преобразования категории метаданных в данные для предсказания.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IDataTransformer{Metadata, int?}"/>.
    /// </remarks>
    public class MetadataCategoriesTransformer : IDataTransformer<TaskMetadata, int?>
    {
        /// <summary>
        /// Категории.
        /// </summary>
        private List<string?> _categories = new();

        /// <inheritdoc/>
        public IEnumerable<int?> FitTransform(IEnumerable<TaskMetadata> data)
        {
            _categories.Clear();
            foreach (var metadata in data)
            {
                var category = metadata.Category;
                if (_categories.Contains(category))
                {
                    _categories.Add(category);
                }
            }
            foreach (var metadata in data)
            {
                yield return Transform(metadata);
            }
        }

        /// <inheritdoc/>
        public int? Transform(TaskMetadata data)
        {
            var index = _categories.IndexOf(data.Category);
            return index != -1 ? index : null;
        }
    }
}
