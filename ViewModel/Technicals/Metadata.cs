using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel.Technicals
{
    public class Metadata : INotifyPropertyChanged
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
            set => OnPropertyChanged(ref _tags, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged<T>(ref T field, T newValue,
            [CallerMemberName] string propertyName = "")
        {
            if ((field != null && !field.Equals(newValue)) ||
                (newValue != null && !newValue.Equals(field)))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
