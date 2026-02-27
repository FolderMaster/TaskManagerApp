using Database.Entities;
using Model.Interfaces;
using Model.Tasks.RecurringTasks;

namespace Database.Mappers
{
    /// <summary>
    /// Класс перобразования значений повторяющейся элементарной задачи
    /// между двумя предметными областями.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMapper{RecurringTaskElementEntity, IRecurringTaskElement}"/>.
    /// </remarks>
    public class RecurringTaskElementMapper :
        IMapper<RecurringTaskElementEntity, IRecurringTaskElement>
    {
        /// <summary>
        /// Преобразование значений между сущностью настроки повторения и настойкой повторения.
        /// </summary>
        private readonly IMapper<RecurringSettingsOwnedEntity, RecurringSettings>
            _recurringSettingsMapper;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="RecurringSettingsMapper"/>.
        /// </summary>
        /// <param name="recurringSettingsMapper">Преобразование значений между сущностью
        /// настройки повторения и настройкой повторения.</param>
        public RecurringTaskElementMapper
            (IMapper<RecurringSettingsOwnedEntity, RecurringSettings> recurringSettingsMapper)
        {
            _recurringSettingsMapper = recurringSettingsMapper;
        }

        /// <inheritdoc/>
        public IRecurringTaskElement Map(RecurringTaskElementEntity value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public RecurringTaskElementEntity MapBack(IRecurringTaskElement value)
        {
            throw new NotImplementedException();
        }
    }
}
