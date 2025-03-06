namespace Model
{
    /// <summary>
    /// Перечисление дней недели.
    /// </summary>
    [Flags]
    public enum WeekDay : byte
    {
        /// <summary>
        /// Никакой день недели.
        /// </summary>
        None = 0,
        /// <summary>
        /// Воскресенье.
        /// </summary>
        Sunday = 1 << 0,
        /// <summary>
        /// Понедельник.
        /// </summary>
        Monday = 1 << 1,
        /// <summary>
        /// Вторник.
        /// </summary>
        Tuesday = 1 << 2,
        /// <summary>
        /// Среда.
        /// </summary>
        Wednesday = 1 << 3,
        /// <summary>
        /// Четверг.
        /// </summary>
        Thursday = 1 << 4,
        /// <summary>
        /// Пятница.
        /// </summary>
        Friday = 1 << 5,
        /// <summary>
        /// Суббота.
        /// </summary>
        Saturday = 1 << 6,
        /// <summary>
        /// Все дни недели.
        /// </summary>
        All = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday
    }
}
