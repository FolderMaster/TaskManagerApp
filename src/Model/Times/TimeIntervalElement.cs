using TrackableFeatures;

using Model.Interfaces;

namespace Model.Times
{
    /// <summary>
    /// Класс элементарного временного интервала. Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="ITimeIntervalElement"/>.
    /// </summary>
    public class TimeIntervalElement : TrackableObject, ITimeIntervalElement
    {
        /// <summary>
        /// Начало.
        /// </summary>
        private DateTime _start;

        /// <summary>
        /// Конец.
        /// </summary>
        private DateTime _end;

        /// <inheritdoc/>
        public DateTime Start
        {
            get => _start;
            set => UpdateProperty(ref _start, value, () => OnPropertyChanged(nameof(Duration)));
        }

        /// <inheritdoc/>
        public DateTime End
        {
            get => _end;
            set => UpdateProperty(ref _end, value, () => OnPropertyChanged(nameof(Duration)));
        }

        /// <inheritdoc/>
        public TimeSpan Duration => End - Start;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeIntervalElement"/>.
        /// </summary>
        /// <param name="start">Начало.</param>
        /// <param name="end">Конец.</param>
        public TimeIntervalElement(DateTime? start = null, DateTime? end = null)
        {
            Start = start ?? DateTime.Now;
            End = end ?? DateTime.Now;
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeIntervalElement"/> по умолчанию.
        /// </summary>
        public TimeIntervalElement() : this(null, null) { }
    }
}
