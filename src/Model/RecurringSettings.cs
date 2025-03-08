using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Model
{
    /// <summary>
    /// Класс настройки повторения.
    /// </summary>
    public class RecurringSettings
    {
        /// <summary>
        /// Возвращает и задаёт частоту повторения.
        /// </summary>
        public TimeSpan Frequency { get; set; } = new TimeSpan(1, 0, 0, 0);

        /// <summary>
        /// Возвращает и задаёт цикл повторения.
        /// </summary>
        public RecurringCycle Cycle { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт дата начала.
        /// </summary>
        public DateTime? StartDate { get; set; } = null;

        /// <summary>
        /// Возвращает и задаёт дата конца.
        /// </summary>
        public DateTime? EndDate { get; set; } = null;

        /// <summary>
        /// Рассчитыввает повторения в периоде.
        /// </summary>
        /// <param name="startDate">Дата начала периода.</param>
        /// <param name="endDate">Дата конца периода.</param>
        /// <returns>Возвращает временные метки повторений в периоде.</returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<DateTime> CalculateOccurrences(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException(nameof(startDate));
            }
            if (startDate > EndDate || endDate <= StartDate || Cycle.HasNoRecurrence())
            {
                return Enumerable.Empty<DateTime>();
            }
            var cycleStartDate = ApplyCycle(startDate);
            var result = new List<DateTime>();
            endDate = EndDate != null && EndDate < endDate ? EndDate.Value : endDate;
            while (cycleStartDate < endDate)
            {
                var cycleEndDate = cycleStartDate + Frequency;
                if (cycleEndDate > endDate)
                {
                    cycleEndDate = endDate;
                }

                if (AdjustDate(cycleStartDate, cycleEndDate, out var date))
                {
                    result.Add(date);
                }

                cycleStartDate = cycleEndDate;
            }
            return result;
        }

        /// <summary>
        /// Применяет заданный цикл к указанной дате.
        /// </summary>
        /// <param name="startDate">Дата начала периода.</param>
        /// <returns>Дата, соответствующая ближайшему допустимому времени в рамках цикла.</returns>
        internal DateTime ApplyCycle(DateTime startDate)
        {
            if (StartDate == null)
            {
                return startDate;
            }
            if (startDate < StartDate)
            {
                return StartDate.Value;
            }
            var dateDifference = startDate - StartDate.Value;
            var count = dateDifference / Frequency;
            var сeilingCount = Math.Ceiling(count);
            var countDifference = сeilingCount - count;
            var adjustedTime = countDifference * Frequency;
            var result = startDate + adjustedTime;
            return result;
        }

        /// <summary>
        /// Подбирает ближайшую минуту, соответствующую правилам цикла.
        /// </summary>
        /// <param name="cycleStartDate">Дата начала цикла.</param>
        /// <param name="cycleEndDate">Дата конца цикла.</param>
        /// <param name="result">Результат, дата, соответствующая критериям.</param>
        /// <returns>Возвращает <c>true</c>, если была найдена дата, соответствующая критериям,
        /// иначе <c>false</c>.</returns>
        internal bool AdjustMinute(DateTime cycleStartDate,
            DateTime cycleEndDate, out DateTime result)
        {
            result = DateTime.MinValue;
            if (Cycle.Minutes == RecurringCycle.ALL_MINUTES ||
                Cycle.MatchesMinute(cycleStartDate.Minute))
            {
                result = cycleStartDate;
                return true;
            }
            var date = cycleStartDate;
            var minute = date.Minute;
            for (var i = 1; i < 60; ++i)
            {
                var newMinute = (minute + i) % 60;
                date = date.AddMinutes(1);
                if (date >= cycleEndDate)
                {
                    return false;
                }
                if (Cycle.MatchesMinute(newMinute))
                {
                    result = date;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Подбирает ближайший час, соответствующий правилам цикла.
        /// </summary>
        /// <param name="cycleStartDate">Дата начала цикла.</param>
        /// <param name="cycleEndDate">Дата конца цикла.</param>
        /// <param name="result">Результат, дата, соответствующая критериям.</param>
        /// <returns>Возвращает <c>true</c>, если была найдена дата, соответствующая критериям,
        /// иначе <c>false</c>.</returns>
        internal bool AdjustHour(DateTime cycleStartDate,
            DateTime cycleEndDate, out DateTime result)
        {
            result = DateTime.MinValue;
            if (Cycle.Hours == RecurringCycle.ALL_HOURS ||
                Cycle.MatchesHour(cycleStartDate.Hour))
            {
                result = cycleStartDate;
                return true;
            }
            var date = cycleStartDate;
            var hour = date.Hour;
            for (var i = 1; i < 24; ++i)
            {
                var newHour = (hour + i) % 24;
                date = date.AddHours(1);
                if (date >= cycleEndDate)
                {
                    return false;
                }
                if (Cycle.MatchesHour(newHour))
                {
                    result = date;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Подбирает ближайшие минуту и час, соответствующие правилам цикла.
        /// </summary>
        /// <param name="cycleStartDate">Дата начала цикла.</param>
        /// <param name="cycleEndDate">Дата конца цикла.</param>
        /// <param name="result">Результат, дата, соответствующая критериям.</param>
        /// <returns>Возвращает <c>true</c>, если была найдена дата, соответствующая критериям,
        /// иначе <c>false</c>.</returns>
        internal bool AdjustMinuteAndHour(DateTime cycleStartDate,
            DateTime cycleEndDate, out DateTime result)
        {
            result = DateTime.MinValue;
            if (!AdjustMinute(cycleStartDate, cycleEndDate, out var date))
            {
                return false;
            }
            if (!AdjustHour(date, cycleEndDate, out date))
            {
                return false;
            }
            result = date;
            return true;
        }

        /// <summary>
        /// Подбирает ближайшую дату, соответствующую правилам цикла.
        /// </summary>
        /// <param name="cycleStartDate">Дата начала цикла.</param>
        /// <param name="cycleEndDate">Дата конца цикла.</param>
        /// <param name="result">Результат, дата, соответствующая критериям.</param>
        /// <returns>Возвращает <c>true</c>, если была найдена дата, соответствующая критериям,
        /// иначе <c>false</c>.</returns>
        internal bool AdjustDate(DateTime cycleStartDate,
            DateTime cycleEndDate, out DateTime result)
        {
            result = DateTime.MinValue;
            if (!AdjustMinuteAndHour(cycleStartDate, cycleEndDate, out var date))
            {
                return false;
            }
            if ((Cycle.WeekDays == WeekDay.All && Cycle.Months == Month.All &&
                Cycle.MonthDays == RecurringCycle.ALL_MONTH_DAYS) ||
                (Cycle.MatchesWeekDay((int)date.DayOfWeek) &&
                Cycle.MatchesMonth(date.Month - 1) &&
                Cycle.MatchesMonthDay(date.Day - 1)))
            {
                result = date;
                return true;
            }
            var isDateChanged = false;
            while (true)
            {
                while (!Cycle.MatchesMonth(date.Month - 1))
                {
                    date = new DateTime(date.Year, date.Month, 1,
                        isDateChanged ? date.Hour : 0, isDateChanged ? date.Minute : 0,
                        date.Second, date.Millisecond, date.Microsecond);
                    if (!isDateChanged)
                    {
                        if (!AdjustMinuteAndHour(date, cycleEndDate, out date))
                        {
                            return false;
                        }
                        isDateChanged = true;
                    }
                    date = date.AddMonths(1);
                    if (date >= cycleEndDate)
                    {
                        return false;
                    }
                }

                var maxDay = DateTime.DaysInMonth(date.Year, date.Month);
                var newDay = date.Day;
                while (newDay <= maxDay)
                {
                    if (date >= cycleEndDate)
                    {
                        return false;
                    }
                    if (Cycle.MatchesMonthDay(date.Day - 1) &&
                        Cycle.MatchesWeekDay((int)date.DayOfWeek))
                    {
                        result = date;
                        return true;
                    }
                    if (!isDateChanged)
                    {
                        date = new DateTime(date.Year, date.Month, date.Day, 0, 0,
                            date.Second, date.Millisecond, date.Microsecond);
                        if (!AdjustMinuteAndHour(date, cycleEndDate, out date))
                        {
                            return false;
                        }
                        isDateChanged = true;
                    }
                    date = date.AddDays(1);
                    ++newDay;
                }
                date = new DateTime(date.Year, date.Month, 1,
                    isDateChanged ? date.Hour : 0, isDateChanged ? date.Minute : 0,
                    date.Second, date.Millisecond, date.Microsecond);
                if (!isDateChanged)
                {
                    if (!AdjustMinuteAndHour(date, cycleEndDate, out date))
                    {
                        return false;
                    }
                    isDateChanged = true;
                }
                date = date.AddMonths(1);
            }
        }
    }
}
