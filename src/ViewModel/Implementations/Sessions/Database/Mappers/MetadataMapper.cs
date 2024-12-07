using ViewModel.Implementations.Sessions.Database.Entities;
using ViewModel.Technicals;

namespace ViewModel.Implementations.Sessions.Database.Mappers
{
    public class MetadataMapper : IMapper<MetadataEntity, object>
    {
        public object Map(MetadataEntity value)
        {
            return new Metadata()
            {
                Title = value.Title,
                Description = value.Description,
                Category = value.Category,
                Tags = value.Tags.Select(t => t.Tag)
            };
        }

        public MetadataEntity MapBack(object value)
        {
            if (value is not Metadata metadata)
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
