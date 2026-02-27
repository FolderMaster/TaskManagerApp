using Microsoft.EntityFrameworkCore;

using Model;

namespace Database.Entities
{
    /// <summary>
    /// Класс встроенной сущности цикла повторения.
    /// </summary>
    [Owned]
    public class RecurringCycleOwnedEntity
    {
        /// <summary>
        /// Возвращает и задаёт дни недели.
        /// </summary>
        public ulong Minutes { get; set; }

        /// <summary>
        /// Возвращает и задаёт дни недели.
        /// </summary>
        public uint Hours { get; set; }

        /// <summary>
        /// Возвращает и задаёт дни недели.
        /// </summary>
        public WeekDay WeekDays { get; set; }

        /// <summary>
        /// Возвращает и задаёт дни месяца.
        /// </summary>
        public uint MonthDays { get; set; }

        /// <summary>
        /// Возвращает и задаёт месяцы.
        /// </summary>
        public Month Months { get; set; }
    }
}
