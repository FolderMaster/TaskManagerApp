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
            while (cycleStartDate > endDate)
            {
                var cycleEndDate = cycleStartDate + Frequency;
                if (cycleEndDate > endDate)
                {
                    cycleEndDate = endDate;
                }

                var date = AdjustDate(cycleStartDate, cycleEndDate);
                if (date == null)
                {
                    break;
                }
                cycleStartDate = date.Value;
                date = AdjustHour(cycleStartDate, cycleEndDate);
                if (date == null)
                {
                    break;
                }
                cycleStartDate = date.Value;
                date = AdjustMinute(cycleStartDate, cycleEndDate);
                if (date == null)
                {
                    break;
                }
                cycleStartDate = date.Value;

                result.Add(cycleStartDate);
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
            var floorCount = Math.Floor(count);
            var countDifference = floorCount - count;
            var adjustedTime = countDifference * Frequency;
            var result = startDate + adjustedTime;
            return result;
        }

        /// <summary>
        /// Подбирает ближайшую минуту, соответствующую правилам цикла.
        /// </summary>
        /// <param name="cycleStartDate">Дата начала цикла.</param>
        /// <param name="cycleEndDate">Дата конца цикла.</param>
        /// <returns>Возвращает дату, соответствующую критериям, инача <c>null</c>.</returns>
        internal DateTime? AdjustMinute(DateTime cycleStartDate, DateTime cycleEndDate)
        {
            if (Cycle.Minutes == RecurringCycle.ALL_MINUTES ||
                Cycle.MatchesMinute(cycleStartDate.Minute))
            {
                return cycleStartDate;
            }
            var date = cycleStartDate;
            var minute = date.Minute;
            for (var i = 1; i < 60; ++i)
            {
                var newMinute = (minute + i) % 60;
                if (Cycle.MatchesMinute(newMinute))
                {
                    date = date.AddMinutes(1);
                    if (date >= cycleEndDate)
                    {
                        return null;
                    }
                    return date;
                }
            }
            return null;
        }

        /// <summary>
        /// Подбирает ближайший час, соответствующий правилам цикла.
        /// </summary>
        /// <param name="cycleStartDate">Дата начала цикла.</param>
        /// <param name="cycleEndDate">Дата конца цикла.</param>
        /// <returns>Возвращает дату, соответствующая критериям, инача <c>null</c>.</returns>
        internal DateTime? AdjustHour(DateTime cycleStartDate, DateTime cycleEndDate)
        {
            if (Cycle.Hours == RecurringCycle.ALL_HOURS ||
                Cycle.MatchesHour(cycleStartDate.Hour))
            {
                return cycleStartDate;
            }
            var date = cycleStartDate;
            var hour = date.Hour;
            for (var i = 1; i < 24; ++i)
            {
                var newHour = (hour + i) % 24;
                if (Cycle.MatchesHour(newHour))
                {
                    date = date.AddHours(1);
                    if (date >= cycleEndDate)
                    {
                        return null;
                    }
                    return date;
                }
            }
            return null;
        }

        /// <summary>
        /// Подбирает ближайшую дату, соответствующую правилам цикла.
        /// </summary>
        /// <param name="cycleStartDate">Дата начала цикла.</param>
        /// <param name="cycleEndDate">Дата конца цикла.</param>
        /// <returns>Возвращает дату, соответствующая критериям, инача <c>null</c>.</returns>
        internal DateTime? AdjustDate(DateTime cycleStartDate, DateTime cycleEndDate)
        {
            if ((Cycle.WeekDays == WeekDay.All && Cycle.Months == Month.All &&
                Cycle.MonthDays == RecurringCycle.ALL_MONTH_DAYS) ||
                (Cycle.MatchesWeekDay((int)cycleStartDate.DayOfWeek) &&
                Cycle.MatchesMonth(cycleStartDate.Month - 1) &&
                Cycle.MatchesMonthDay(cycleStartDate.Day - 1)))
            {
                return cycleStartDate;
            }
            var date = cycleStartDate;
            while (date < cycleEndDate)
            {
                while (!Cycle.MatchesMonth(date.Month - 1))
                {
                    date = new DateTime(date.Year, date.Month, 1, date.Hour, date.Minute,
                        date.Second, date.Millisecond, date.Microsecond).AddMonths(1);
                    if (date >= cycleEndDate)
                    {
                        break;
                    }
                }

                var maxDay = DateTime.DaysInMonth(date.Year, date.Month);
                var newDay = date.Day;
                while (newDay <= maxDay)
                {
                    date = new DateTime(date.Year, date.Month, newDay, date.Hour,
                        date.Minute, date.Second, date.Millisecond, date.Microsecond);
                    if (date >= cycleEndDate)
                    {
                        return null;
                    }
                    if (Cycle.MatchesMonthDay(date.Day - 1) && Cycle.MatchesWeekDay((int)date.DayOfWeek))
                    {
                        return date;
                    }
                    ++newDay;
                }
                date = new DateTime(date.Year, date.Month, 1, date.Hour, date.Minute,
                        date.Second, date.Millisecond, date.Microsecond).AddMonths(1);

            }
            return null;
        }
    }
}
