using System.ComponentModel;

namespace Model
{
    public class TimeIntervalCollection : TrackableCollection<ITimeInterval>, ITimeInterval
    {
        public TimeSpan Duration => _items.Aggregate(TimeSpan.Zero, (s, i) => s + i.Duration);

        public TimeIntervalCollection(IEnumerable<ITimeInterval>? timeIntervals = null) :
            base(timeIntervals) { }

        protected override void OnAddedItem(ITimeInterval timeInterval,
            bool arePropertiesUpdate = true)
        {
            if (timeInterval is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += Notify_PropertyChanged;
            }
            if (arePropertiesUpdate)
            {
                OnPropertyChanged(nameof(Duration));
            }
        }

        protected override void OnRemovedItem(ITimeInterval timeInterval)
        {
            if (timeInterval is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged -= Notify_PropertyChanged;
            }
            OnPropertyChanged(nameof(Duration));
        }

        private void Notify_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Duration))
            {
                OnPropertyChanged(e.PropertyName);
            }
        }
    }
}
