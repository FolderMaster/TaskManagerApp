using Database.Entities;
using Model.Tasks.RecurringTasks;

namespace Database.Mappers
{
    /// <summary>
    /// Класс перобразования значений настроек повторения между двумя предметными областями.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMapper{RecurringSettingsOwnedEntity, RecurringSettings}"/>.
    /// </remarks>
    public class RecurringSettingsMapper : IMapper<RecurringSettingsOwnedEntity, RecurringSettings>
    {
        /// <summary>
        /// Преобразование значений между сущностью цикла повторения и циклом повторения.
        /// </summary>
        private readonly IMapper<RecurringCycleOwnedEntity, RecurringCycle> _recurringCycleMapper;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="RecurringSettingsMapper"/>.
        /// </summary>
        /// <param name="recurringCycleMapper">Преобразование значений между сущностью цикла 
        /// повторения и циклом повторения.</param>
        public RecurringSettingsMapper
            (IMapper<RecurringCycleOwnedEntity, RecurringCycle> recurringCycleMapper)
        {
            _recurringCycleMapper = recurringCycleMapper;
        }

        /// <inheritdoc/>
        public RecurringSettings Map(RecurringSettingsOwnedEntity value)
        {
            return new RecurringSettings()
            {
                Cycle = _recurringCycleMapper.Map(value.Cycle),
                EndDate = value.EndDate,
                StartDate = value.StartDate,
                Frequency = value.Frequency
            };
        }

        /// <inheritdoc/>
        public RecurringSettingsOwnedEntity MapBack(RecurringSettings value)
        {
            if (value.Cycle == null)
            {
                throw new ArgumentException(nameof(value));
            }
            return new RecurringSettingsOwnedEntity()
            {
                Cycle = _recurringCycleMapper.MapBack(value.Cycle),
                EndDate = value.EndDate,
                StartDate = value.StartDate,
                Frequency = value.Frequency
            };
        }
    }
}
