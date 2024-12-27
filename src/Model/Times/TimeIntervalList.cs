using System.ComponentModel;

using TrackableFeatures;

using Model.Interfaces;

namespace Model.Times
{
    /// <summary>
    /// Класс списка временных интервалов.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableCollection{ITimeIntervalElement}"/>.
    /// Реализует <see cref="ITimeIntervalList"/>.
    /// </remarks>
    public class TimeIntervalList : TrackableCollection<ITimeIntervalElement>, ITimeIntervalList
    {
        /// <inheritdoc/>
        public TimeSpan Duration => this.Aggregate(TimeSpan.Zero, (s, i) => s + i.Duration);

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeIntervalList"/>.
        /// </summary>
        /// <param name="timeIntervals">Временные интервалы.</param>
        public TimeIntervalList(IEnumerable<ITimeIntervalElement>? timeIntervals = null) :
            base(timeIntervals) { }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeIntervalList"/> по умолчанию.
        /// </summary>
        public TimeIntervalList() : this(null) { }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
