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
            set => OnPropertyChanged(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => OnPropertyChanged(ref _description, value);
        }

        public object Category
        {
            get => _category;
            set => OnPropertyChanged(ref _category, value);
        }

        public IEnumerable<object> Tags
        {
            get => _tags;
            set => OnPropertyChanged(ref _tags, value.Distinct());
        }

        public override string ToString() => $"{Name}";
    }
}
