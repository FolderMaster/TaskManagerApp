using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.Technicals
{
    public class TrackableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void UpdateProperty<T>(ref T field, T newValue, Action? action = null,
            [CallerMemberName] string propertyName = "")
        {
            if (field != null && !field.Equals(newValue) ||
                newValue != null && !newValue.Equals(field))
            {
                field = newValue;
                action?.Invoke();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
