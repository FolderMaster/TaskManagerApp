using System.ComponentModel;

using Model.Interfaces;
using Model.Technicals;

namespace Model.Tasks.Ranges
{
    public abstract class CompositeRangeValue<T> : TrackableCollection<IReadonlyRangeValue<T>>,
        IReadonlyRangeValue<T>
    {
        public abstract T Value { get; }

        public abstract T Min { get; }

        public abstract T Max { get; }

        public CompositeRangeValue(IEnumerable<IReadonlyRangeValue<T>>? items = null) :
            base(items) { }

        protected override void OnAddedItem(IReadonlyRangeValue<T> rangeValue,
            bool arePropertiesUpdate = true)
        {
            if (rangeValue is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += Notify_PropertyChanged;
            }
            if (arePropertiesUpdate)
            {
                UpdateProperties();
            }
        }

        protected override void OnRemovedItem(IReadonlyRangeValue<T> rangeValue)
        {
            if (rangeValue is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged -= Notify_PropertyChanged;
            }
            UpdateProperties();
        }

        protected abstract void UpdateProperties();

        protected abstract void UpdateProperty(string? propertyName);

        private void Notify_PropertyChanged(object? sender, PropertyChangedEventArgs e) =>
            UpdateProperty(e.PropertyName);
    }
}
