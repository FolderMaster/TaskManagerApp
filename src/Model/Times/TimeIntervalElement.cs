using System.Runtime.CompilerServices;

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
            set => UpdateProperty(ref _start, value, () => UpdateDateTimeProperty(nameof(Start)));
        }

        /// <inheritdoc/>
        public DateTime End
        {
            get => _end;
            set => UpdateProperty(ref _end, value, () => UpdateDateTimeProperty(nameof(End)));
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

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/> и обносляет ошибки для свойств
        /// <see cref="Start"/> или <see cref="End"/>.
        /// </summary>
        /// <param name="propertyName">Название свойства.</param>
        private void UpdateDateTimeProperty([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(propertyName);
            ClearAllErrors();
            if (Start > End)
            {
                if (propertyName == nameof(Start))
                {
                    AddError($"{nameof(Start)} находится после {nameof(End)}", nameof(Start));
                }
                else if (propertyName == nameof(End))
                {
                    AddError($"{nameof(End)} находится до {nameof(Start)}", nameof(End));
                }
            }
            OnPropertyChanged(nameof(Duration));
        }
    }
}
