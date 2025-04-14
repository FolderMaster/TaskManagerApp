using Microsoft.EntityFrameworkCore;

namespace Database.Entities
{
    /// <summary>
    /// Класс встроенной сущности настройки повторения.
    /// </summary>
    [Owned]
    public class RecurringSettingsOwnedEntity
    {
        /// <summary>
        /// Возвращает и задаёт частоту повторения.
        /// </summary>
        public TimeSpan Frequency { get; set; }

        /// <summary>
        /// Возвращает и задаёт цикл повторения.
        /// </summary>
        public RecurringCycleOwnedEntity Cycle { get; set; }

        /// <summary>
        /// Возвращает и задаёт дата начала.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Возвращает и задаёт дата конца.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
