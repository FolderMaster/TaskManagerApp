using Model;

namespace ViewModel.Technicals
{
    public class Metadata : ObservableObject
    {
        private string _name;

        private string _description;

        private object _category;

        private IEnumerable<object> _tags;

        public string Name
        {
            get => _name;
            set => UpdateProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => UpdateProperty(ref _description, value);
        }

        public object Category
        {
            get => _category;
            set => UpdateProperty(ref _category, value);
        }

        public IEnumerable<object> Tags
        {
            get => _tags;
            set => UpdateProperty(ref _tags, value.Distinct().ToList());
        }

        public override string ToString() => $"{Name}";
    }
}
