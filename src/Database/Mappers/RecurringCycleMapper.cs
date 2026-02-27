using Database.Entities;
using Model.Tasks.RecurringTasks;

namespace Database.Mappers
{
    /// <summary>
    /// Класс перобразования значений цикла повторения между двумя предметными областями.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMapper{RecurringCycleOwnedEntity, RecurringCycle}"/>.
    /// </remarks>
    public class RecurringCycleMapper : IMapper<RecurringCycleOwnedEntity, RecurringCycle>
    {
        /// <inheritdoc/>
        public RecurringCycle Map(RecurringCycleOwnedEntity value)
        {
            return new RecurringCycle()
            {
                Hours = value.Hours,
                Minutes = value.Minutes,
                WeekDays = value.WeekDays,
                MonthDays = value.MonthDays,
                Months = value.Months
            };
        }

        /// <inheritdoc/>
        public RecurringCycleOwnedEntity MapBack(RecurringCycle value)
        {
            return new RecurringCycleOwnedEntity()
            {
                Hours = value.Hours,
                Minutes = value.Minutes,
                WeekDays = value.WeekDays,
                MonthDays = value.MonthDays,
                Months = value.Months
            };
        }
    }
}
