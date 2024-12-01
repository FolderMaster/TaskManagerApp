using System.ComponentModel;

using TrackableFeatures;

using Model.Interfaces;

namespace Model.Times
{
    public class TimeIntervalList : TrackableCollection<ITimeIntervalElement>, ITimeIntervalList
    {
        public TimeSpan Duration => _items.Aggregate(TimeSpan.Zero, (s, i) => s + i.Duration);

        public TimeIntervalList(IEnumerable<ITimeIntervalElement>? timeIntervals = null) :
            base(timeIntervals) { }

        public TimeIntervalList() : this(null) { }

        protected override void OnAddedItem(ITimeIntervalElement timeInterval,
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

        protected override void OnRemovedItem(ITimeIntervalElement timeInterval)
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
