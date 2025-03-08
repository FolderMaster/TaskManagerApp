using Common.Tests;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace Model.Tests
{
    [Level(TestLevel.Unit)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Major)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(RecurringCycle),
        Description = $"Тестирование класса {nameof(RecurringCycle)}.")]
    public class RecurringCycleTests
    {
        private RecurringCycle _cycle;

        [SetUp]
        public void Setup()
        {
            _cycle = new();
        }

        [Combinatorial]
        [Test(Description = $"Тестирование метода {nameof(RecurringCycle.MatchesMinute)} " +
            "при установлении минуты.")]
        public void MatchesMinute_SetMinute_ReturnsTrue([Range(0, 59)] int minute)
        {
            _cycle.Minutes = 1UL << minute;
            var result = _cycle.MatchesMinute(minute);

            Assert.That(result, Is.True, "Неправильная проверка минуты!");
        }

        [Combinatorial]
        [Test(Description = $"Тестирование метода {nameof(RecurringCycle.MatchesHour)} " +
            "при установлении часа.")]
        public void MatchesHour_SetHour_ReturnsTrue([Range(0, 23)] int hour)
        {
            _cycle.Hours = 1U << hour;
            var result = _cycle.MatchesHour(hour);

            Assert.That(result, Is.True, "Неправильная проверка часа!");
        }

        [Combinatorial]
        [Test(Description = $"Тестирование метода {nameof(RecurringCycle.MatchesWeekDay)} " +
            "при установлении дня недели.")]
        public void MatchesWeekDay_SetWeekDay_ReturnsTrue([Range(0, 6)] int weekDay)
        {
            _cycle.WeekDays = (WeekDay)(1 << weekDay);
            var result = _cycle.MatchesWeekDay(weekDay);

            Assert.That(result, Is.True, "Неправильная проверка дня недели!");
        }

        [Combinatorial]
        [Test(Description = $"Тестирование метода {nameof(RecurringCycle.MatchesMonthDay)} " +
            "при установлении дня месяца.")]
        public void MatchesMonthDay_SetMonthDay_ReturnsTrue([Range(0, 30)] int monthDay)
        {
            _cycle.MonthDays = 1U << monthDay;
            var result = _cycle.MatchesMonthDay(monthDay);

            Assert.That(result, Is.True, "Неправильная проверка дня месяца!");
        }

        [Combinatorial]
        [Test(Description = $"Тестирование метода {nameof(RecurringCycle.MatchesMonth)} " +
            "при установлении месяца.")]
        public void MatchesMonth_SetMonth_ReturnsTrue([Range(0, 11)] int month)
        {
            _cycle.Months = (Month)(1 << month);
            var result = _cycle.MatchesMonth(month);

            Assert.That(result, Is.True, "Неправильная проверка месяца!");
        }

        [Test(Description = $"Тестирование метода {nameof(RecurringCycle.MatchesDate)} " +
            "при установлении свойств.")]
        public void MatchesDate_SetProperties_ReturnsTrue()
        {
            var year = 2025;
            var month = 2;
            var monthDay = 6;
            var weekDay = 5;
            var hour = 16;
            var minute = 1;
            var date = new DateTime(year, month + 1, monthDay + 1, hour, minute, 0);

            _cycle.Minutes = 1UL << minute;
            _cycle.Hours = 1U << hour;
            _cycle.WeekDays = (WeekDay)(1 << weekDay);
            _cycle.MonthDays = 1U << monthDay;
            _cycle.Months = (Month)(1 << month);
            var result = _cycle.MatchesDate(date);

            Assert.That(result, Is.True, "Неправильная проверка даты!");
        }

        [Combinatorial]
        [Test(Description = $"Тестирование метода {nameof(RecurringCycle.HasNoRecurrence)} " +
            "при установлении свойств.")]
        public void HasNoRecurrence_SetProperties_ReturnsCorrectResult([Values] bool month,
            [Values] bool monthDay, [Values] bool weekDay,
            [Values] bool hour, [Values] bool minute)
        {
            var expected = minute || hour || weekDay || monthDay || month;

            _cycle.Minutes = minute ? 0 : RecurringCycle.ALL_MINUTES;
            _cycle.Hours = hour ? 0 : RecurringCycle.ALL_HOURS;
            _cycle.WeekDays = weekDay ? WeekDay.None : WeekDay.All;
            _cycle.MonthDays = monthDay ? 0 : RecurringCycle.ALL_MONTH_DAYS;
            _cycle.Months = month ? Month.None : Month.All;
            var result = _cycle.HasNoRecurrence();

            Assert.That(result, Is.EqualTo(expected), "Неправильная проверка даты!");
        }
    }
}
