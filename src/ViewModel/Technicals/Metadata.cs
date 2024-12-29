using TrackableFeatures;

namespace ViewModel.Technicals
{
    public class Metadata : TrackableObject, ICloneable
    {
        private string _title;

        private string? _description;

        private string? _category;

        private IEnumerable<string> _tags;

        public string Title
        {
            get => _title;
            set => UpdateProperty(ref _title, value);
        }

        public string? Description
        {
            get => _description;
            set => UpdateProperty(ref _description, value);
        }

        public string? Category
        {
            get => _category;
            set => UpdateProperty(ref _category, value);
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            set => UpdateProperty(ref _tags, value.Distinct().ToList());
        }

        public object Clone() => new Metadata()
        {
            Title = Title,
            Description = Description,
            Category = Category,
            Tags = Tags.ToList()
        };

        public override string ToString() => $"{Title}";
    }
}
