using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public class MetadataCategoriesTransformer : IDataTransformer<Metadata, int?>
    {
        private List<string?> _categories = new();

        public IEnumerable<int?> FitTransform(IEnumerable<Metadata> data)
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

        public int? Transform(Metadata data)
        {
            var index = _categories.IndexOf(data.Category);
            return index != -1 ? index : null;
        }
    }
}
