using Model.Times;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Domains
{
    /// <summary>
    /// Класс домменной модели элементарного временного интервала.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TimeIntervalElement"/>.
    /// Реализует <see cref="IDomain"/>.
    /// </remarks>
    public class TimeIntervalElementDomain : TimeIntervalElement, IDomain
    {
        /// <summary>
        /// Возвращает и задаёт связанную сущность.
        /// </summary>
        public TimeIntervalEntity Entity { get; set; }

        /// <inheritdoc/>
        public object EntityId => Entity;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeIntervalElementDomain"/>.
        /// </summary>
        /// <param name="start">Начало.</param>
        /// <param name="end">Конец.</param>
        public TimeIntervalElementDomain(DateTime? start = null, DateTime? end = null) :
            base(start, end)
        { }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeIntervalElementDomain"/> по умолчанию.
        /// </summary>
        public TimeIntervalElementDomain() : this(null, null) { }
    }
}
