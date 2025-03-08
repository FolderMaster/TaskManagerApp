namespace Model
{
    /// <summary>
    /// Класс цикла повторения.
    /// </summary>
    public class RecurringCycle
    {
        /// <summary>
        /// Все минуты.
        /// </summary>
        public const ulong ALL_MINUTES = 0xFFFFFFFFFFFFFFF;

        /// <summary>
        /// Все минуты.
        /// </summary>
        public const uint ALL_HOURS = 0xFFFFFF;

        /// <summary>
        /// Все дни месяца.
        /// </summary>
        public const uint ALL_MONTH_DAYS = 0x7FFFFFFF;

        /// <summary>
        /// Возвращает и задаёт дни недели.
        /// </summary>
        public ulong Minutes { get; set; } = ALL_MINUTES;

        /// <summary>
        /// Возвращает и задаёт дни недели.
        /// </summary>
        public uint Hours { get; set; } = ALL_HOURS;

        /// <summary>
        /// Возвращает и задаёт дни недели.
        /// </summary>
        public WeekDay WeekDays { get; set; } = WeekDay.All;

        /// <summary>
        /// Возвращает и задаёт дни месяца.
        /// </summary>
        public uint MonthDays { get; set; } = ALL_MONTH_DAYS;

        /// <summary>
        /// Возвращает и задаёт месяцы.
        /// </summary>
        public Month Months { get; set; } = Month.All;

        /// <summary>
        /// Проверяет, что цикл не имеет значения.
        /// </summary>
        /// <returns>Возвращает <c>true</c>, если цикл не имеет значений,
        /// иначе <c>false</c>.</returns>
        public bool HasNoRecurrence() => Minutes == 0 || Hours == 0 ||
            WeekDays == WeekDay.None || MonthDays == 0 || Months == Month.None;

        /// <summary>
        /// Проверяет, что попадает ли минута в цикл.
        /// </summary>
        /// <param name="minute">Минута.</param>
        /// <returns>Возвращает <c>true</c>, если попадает минута в цикл,
        /// иначе <c>false</c>.</returns>
        public bool MatchesMinute(int minute) => (Minutes & (1UL << minute)) != 0;

        /// <summary>
        /// Проверяет, что попадает ли час в цикл.
        /// </summary>
        /// <param name="hour">Час.</param>
        /// <returns>Возвращает <c>true</c>, если попадает час в цикл,
        /// иначе <c>false</c>.</returns>
        public bool MatchesHour(int hour) => (Hours & (1U << hour)) != 0;

        /// <summary>
        /// Проверяет, что попадает ли день недели в цикл.
        /// </summary>
        /// <param name="weekDay">День недели.</param>
        /// <returns>Возвращает <c>true</c>, если попадает день недели в цикл,
        /// иначе <c>false</c>.</returns>
        public bool MatchesWeekDay(int weekDay) => (WeekDays & (WeekDay)(1 << weekDay)) != 0;

        /// <summary>
        /// Проверяет, что попадает ли день месяца в цикл.
        /// </summary>
        /// <param name="monthDay">День месяца.</param>
        /// <returns>Возвращает логическое значение,
        /// указывающее попадает ли день месяца в цикл.</returns>
        public bool MatchesMonthDay(int monthDay) => (MonthDays & (1U << monthDay)) != 0;

        /// <summary>
        /// Проверяет, что попадает ли месяц в цикл.
        /// </summary>
        /// <param name="month">Месяц.</param>
        /// <returns>Возвращает <c>true</c>, если попадает месяц в цикл,
        /// иначе <c>false</c>.</returns>
        public bool MatchesMonth(int month) => (Months & (Month)(1 << month)) != 0;

        /// <summary>
        /// Проверяет, что попадает ли дата в цикл.
        /// </summary>
        /// <param name="dateTime">Дата и время.</param>
        /// <returns>Возвращает <c>true</c>, если попадает дата в цикл,
        /// иначе <c>false</c>.</returns>
        public bool MatchesDate(DateTime dateTime) => MatchesMonth(dateTime.Month - 1) &&
            MatchesWeekDay((int)dateTime.DayOfWeek) && MatchesMonthDay(dateTime.Day - 1) &&
            MatchesHour(dateTime.Hour) && MatchesMinute(dateTime.Minute);
    }
}
