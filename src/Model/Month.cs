namespace Model
{
    /// <summary>
    /// Перечисление месяцев.
    /// </summary>
    [Flags]
    public enum Month : ushort
    {
        /// <summary>
        /// Никакой месяц.
        /// </summary>
        None = 0,
        /// <summary>
        /// Январь.
        /// </summary>
        January = 1 << 0,
        /// <summary>
        /// Февраль.
        /// </summary>
        February = 1 << 1,
        /// <summary>
        /// Март.
        /// </summary>
        March = 1 << 2,
        /// <summary>
        /// Апрель.
        /// </summary>
        April = 1 << 3,
        /// <summary>
        /// Май.
        /// </summary>
        May = 1 << 4,
        /// <summary>
        /// Июнь.
        /// </summary>
        June = 1 << 5,
        /// <summary>
        /// Июль.
        /// </summary>
        July = 1 << 6,
        /// <summary>
        /// Август.
        /// </summary>
        August = 1 << 7,
        /// <summary>
        /// Сентябрь.
        /// </summary>
        September = 1 << 8,
        /// <summary>
        /// Октябрь.
        /// </summary>
        October = 1 << 9,
        /// <summary>
        /// Ноябрь.
        /// </summary>
        November = 1 << 10,
        /// <summary>
        /// Декабрь.
        /// </summary>
        December = 1 << 11,
        /// <summary>
        /// Все месяцы.
        /// </summary>
        All = January | February | March | April | May | June |
          July | August | September | October | November | December
    }
}
