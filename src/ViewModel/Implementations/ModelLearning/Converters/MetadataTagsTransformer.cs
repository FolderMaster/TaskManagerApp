using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public class MetadataTagsTransformer : IDataTransformer<Metadata, IEnumerable<int>>
    {
        private List<string> _tags = new();

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

        public IEnumerable<int> Transform(Metadata data)
        {
            foreach (var tag in _tags)
            {
                yield return data.Tags.Contains(tag) ? 1 : 0;
            }
        }
    }
}
