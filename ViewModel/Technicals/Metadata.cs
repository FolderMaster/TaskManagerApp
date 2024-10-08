using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel.Technicals
{
    public class Metadata : INotifyPropertyChanged
    {
        private string _name;

        private string _description;

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
