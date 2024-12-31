using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    /// <summary>
    /// Класс преобразования тегов метаданных в данные для предсказания.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IDataTransformer{Metadata, IEnumerable{int}}"/>.
    /// </remarks>
    public class MetadataTagsTransformer : IDataTransformer<Metadata, IEnumerable<int>>
    {
        /// <summary>
        /// Теги.
        /// </summary>
        private List<string> _tags = new();

        /// <inheritdoc/>
        public IEnumerable<IEnumerable<int>> FitTransform(IEnumerable<Metadata> data)
        {
            foreach (var metadata in data)
            {
                foreach (var tag in metadata.Tags)
                {
                    if (_tags.Contains(tag))
                    {
                        _tags.Add(tag);
                    }
                }
            }
            foreach (var metadata in data)
            {
                yield return Transform(metadata);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<int> Transform(Metadata data)
        {
            foreach (var tag in _tags)
            {
                yield return data.Tags.Contains(tag) ? 1 : 0;
            }
        }
    }
}
