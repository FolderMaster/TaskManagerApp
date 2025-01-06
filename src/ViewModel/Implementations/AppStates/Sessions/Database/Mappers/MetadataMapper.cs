using ViewModel.Implementations.AppStates.Sessions.Database.Entities;
using ViewModel.Implementations.Sessions.Database.Entities;
using ViewModel.Technicals;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Mappers
{
    /// <summary>
    /// Класс перобразования значений метаданных между двумя предметными областями.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMapper{MetadataEntity, object}"/>.
    /// </remarks>
    public class MetadataMapper : IMapper<MetadataEntity, object>
    {
        /// <inheritdoc/>
        public object Map(MetadataEntity value)
        {
            return new TaskMetadata()
            {
                Title = value.Title,
                Description = value.Description,
                Category = value.Category,
                Tags = value.Tags.Select(t => t.Tag)
            };
        }

        /// <inheritdoc/>
        public MetadataEntity MapBack(object value)
        {
            if (value is not TaskMetadata metadata)
            {
                throw new ArgumentException(nameof(value));
            }
            var result = new MetadataEntity()
            {
                Title = metadata.Title,
                Description = metadata.Description,
                Category = metadata.Category
            };
            result.Tags = metadata.Tags.Select(t => new TagEntity()
            {
                Metadata = result,
                Tag = t
            }).ToList();
            return result;
        }
    }
}
