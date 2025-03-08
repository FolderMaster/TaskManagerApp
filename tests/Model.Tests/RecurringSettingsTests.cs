using Common.Tests;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace Model.Tests
{
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Major)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [TestFixture(TestOf = typeof(RecurringSettings),
        Description = $"Тестирование класса {nameof(RecurringSettings)}.")]
    public class RecurringSettingsTests
    {
        private RecurringSettings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = new();
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(RecurringSettings.CalculateOccurrences)} при дате начала периода, " +
            "которая идёт после даты конца.")]
        public void CalculateOccurrences_StartDateAfterEndDate_ThrowsArgumentException()
        {
            var now = DateTime.Now;
            var startDate = now.AddDays(1);
            var endDate = now;

            Assert.Throws<ArgumentException>(() =>
                _settings.CalculateOccurrences(startDate, endDate),
                "Должно быть выброшено исключение!");
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(RecurringSettings.CalculateOccurrences)} при дате старта периода большей " +
            "даты конца настроек повторения.")]
        public void CalculateOccurrences_StartDateIsMoreSettingsEndDate_ReturnsEmpty()
        {
            var now = DateTime.Now;
            var startDate = now;
            var endDate = now.AddDays(10);
            var settingsEndDate = now.AddDays(-1);

            _settings.EndDate = settingsEndDate;
            var result = _settings.CalculateOccurrences(startDate, endDate);

            Assert.That(result, Is.Empty, "Неправильно расчитаны повторения!");
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(RecurringSettings.CalculateOccurrences)} при дате конца периода равной " +
            "дате начала настроек повторения.")]
        public void CalculateOccurrences_EndDateEqualsSettingsStartDate_ReturnsEmpty()
        {
            var now = DateTime.Now;
            var startDate = now;
            var endDate = now.AddDays(10);
            var settingsStartDate = now.AddDays(10);

            _settings.StartDate = settingsStartDate;
            var result = _settings.CalculateOccurrences(startDate, endDate);

            Assert.That(result, Is.Empty, "Неправильно расчитаны повторения!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(RecurringSettings.CalculateOccurrences)} при отсутствии в цикле повторов.")]
        public void CalculateOccurrences_NoCycleRecurrence_ReturnsEmpty()
        {
            var now = DateTime.Now;
            var startDate = now;
            var endDate = now.AddDays(10);

            _settings.Cycle.WeekDays = WeekDay.None;
            var result = _settings.CalculateOccurrences(startDate, endDate);

            Assert.That(result, Is.Empty, "Неправильно расчитаны повторения!");
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование метода {nameof(RecurringSettings.ApplyCycle)} " +
            "при дате начале периода меньшей даты начала в настройке.")]
        public void ApplyCycle_StartDateIsLessSettingsStartDate_ReturnsStartDate()
        {
            var now = DateTime.Now;
            var settingsStartDate = now;
            var startDate = now.AddDays(-1);

            _settings.StartDate = settingsStartDate;
            var result = _settings.ApplyCycle(startDate);

            Assert.That(result, Is.EqualTo(settingsStartDate), "Неправильно применение цикла!");
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование метода {nameof(RecurringSettings.ApplyCycle)} " +
            "при отсутствии даты начала в настройке.")]
        public void ApplyCycle_SettingsStartDateIsNull_ReturnsStartDate()
        {
            var now = DateTime.Now;
            var startDate = now.AddDays(-1);

            _settings.StartDate = null;
            var result = _settings.ApplyCycle(startDate);

            Assert.That(result, Is.EqualTo(startDate), "Неправильно применение цикла!");
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование метода {nameof(RecurringSettings.ApplyCycle)} " +
            "при дате начале периода большей даты начала в настройке.")]
        public void ApplyCycle_StartDateIsMoreSettingsStartDate_ReturnsAdjustedValue()
        {
            var now = DateTime.Now;
            var settingsStartDate = now;
            var startDate = now.AddDays(4);
            var frequency = TimeSpan.FromDays(3);
            var expected = now.AddDays(6);

            _settings.StartDate = settingsStartDate;
            _settings.Frequency = frequency;
            var result = _settings.ApplyCycle(startDate);
            Assert.That(result, Is.EqualTo(expected), "Неправильно применение цикла!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование метода {nameof(RecurringSettings.AdjustMinute)} " +
            "при установлении минуты.")]
        public void AdjustMinute_SetCycleMinutes_ReturnAdjustedValue()
        {
            var startDate = new DateTime(2025, 3, 7, 12, 0, 0);
            var endDate = DateTime.MaxValue;
            var minute = 30;
            var expected = new DateTime(2025, 3, 7, 12, 30, 0);

            _settings.Cycle.Minutes = 1UL << minute;
            _settings.AdjustMinute(startDate, endDate, out var result);

            Assert.That(result, Is.EqualTo(expected), "Неправильно подобрана ближайшая минута!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование метода {nameof(RecurringSettings.AdjustMinute)} " +
            "при установлении часа с ранней датой конца.")]
        public void AdjustMinute_SetCycleMinutesAndEarlyEndDate_ReturnFalse()
        {
            var startDate = new DateTime(2025, 3, 7, 12, 0, 0);
            var endDate = new DateTime(2025, 3, 7, 12, 20, 0);
            var minute = 30;

            _settings.Cycle.Minutes = 1UL << minute;
            var result = _settings.AdjustMinute(startDate, endDate, out var date);

            Assert.That(result, Is.False, "Неправильно подобрана ближайшая дата!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование метода {nameof(RecurringSettings.AdjustHour)} " +
            "при установлении часа.")]
        public void AdjustHour_SetCycleHours_ReturnAdjustedValue()
        {
            var startDate = new DateTime(2025, 3, 7, 12, 30, 0);
            var endDate = DateTime.MaxValue;
            var hour = 4;
            var expected = new DateTime(2025, 3, 8, 4, 30, 0);

            _settings.Cycle.Hours = 1U << hour;
            _settings.AdjustHour(startDate, endDate, out var result);

            Assert.That(result, Is.EqualTo(expected), "Неправильно подобрана ближайший час!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование метода {nameof(RecurringSettings.AdjustHour)} " +
            "при установлении часа с ранней датой конца.")]
        public void AdjustHour_SetCycleHoursAndEarlyEndDate_ReturnFalse()
        {
            var startDate = new DateTime(2025, 3, 7, 12, 30, 0);
            var endDate = new DateTime(2025, 3, 7, 23, 0, 0);
            var hour = 4;

            _settings.Cycle.Hours = 1U << hour;
            var result = _settings.AdjustHour(startDate, endDate, out var date);

            Assert.That(result, Is.False, "Неправильно подобрана ближайшая дата!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование метода {nameof(RecurringSettings.AdjustDate)} " +
            "при установлении свойств.")]
        public void AdjustDate_SetCycleProperties_ReturnAdjustedValue()
        {
            var startDate = new DateTime(2025, 3, 7, 12, 30, 0);
            var endDate = DateTime.MaxValue;
            var month = 3;
            var monthDay = 11;
            var weekDay = 6;
            var expected = new DateTime(2025, 4, 12, 0, 0, 0);

            _settings.Cycle.WeekDays = (WeekDay)(1 << weekDay);
            _settings.Cycle.MonthDays = 1U << monthDay;
            _settings.Cycle.Months = (Month)(1 << month);
            _settings.AdjustDate(startDate, endDate, out var result);

            Assert.That(result, Is.EqualTo(expected), "Неправильно подобрана ближайшая дата!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование метода {nameof(RecurringSettings.AdjustDate)} " +
            "при установлении свойств с ранней датой конца.")]
        public void AdjustDate_SetCyclePropertiesAndEarlyEndDate_ReturnFalse()
        {
            var startDate = new DateTime(2025, 3, 7, 12, 30, 0);
            var endDate = new DateTime(2025, 3, 12, 13, 30, 0);
            var month = 3;
            var monthDay = 11;
            var weekDay = 6;

            _settings.Cycle.WeekDays = (WeekDay)(1 << weekDay);
            _settings.Cycle.MonthDays = 1U << monthDay;
            _settings.Cycle.Months = (Month)(1 << month);
            var result = _settings.AdjustDate(startDate, endDate, out var date);

            Assert.That(result, Is.False, "Неправильно подобрана ближайшая дата!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(RecurringSettings.CalculateOccurrences)} при установке частоты.")]
        public void CalculateOccurrences_SetFrequency_ReturnsCorrectData()
        {
            var now = DateTime.Now;
            var startDate = now;
            var endDate = now.AddDays(10);
            var frequency = TimeSpan.FromDays(3);
            var expected = new DateTime[] { now, now.AddDays(3), now.AddDays(6), now.AddDays(9) };

            _settings.Frequency = frequency;
            var result = _settings.CalculateOccurrences(startDate, endDate);

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитаны повторения!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(RecurringSettings.CalculateOccurrences)} " +
            "при дате начала настроек большей даты начала периода.")]
        public void CalculateOccurrences_SettingsStartDateIsMoreStartDate_ReturnsCorrectData()
        {
            var now = DateTime.Now;
            var startDate = now;
            var settingsStartDate = now.AddDays(1);
            var endDate = now.AddDays(10);
            var frequency = TimeSpan.FromDays(3);
            var expected = new DateTime[] { now.AddDays(1), now.AddDays(4), now.AddDays(7) };

            _settings.StartDate = settingsStartDate;
            _settings.Frequency = frequency;
            var result = _settings.CalculateOccurrences(startDate, endDate);

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитаны повторения!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Instant)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(RecurringSettings.CalculateOccurrences)} " +
            "при дате конца настроек меньшей даты конца периода.")]
        public void CalculateOccurrences_SettingsEndDateIsLessEndDate_ReturnsCorrectData()
        {
            var now = DateTime.Now;
            var startDate = now;
            var endDate = now.AddDays(10);
            var settingsEndDate = now.AddDays(9);
            var frequency = TimeSpan.FromDays(3);
            var expected = new DateTime[] { now, now.AddDays(3), now.AddDays(6) };

            _settings.EndDate = settingsEndDate;
            _settings.Frequency = frequency;
            var result = _settings.CalculateOccurrences(startDate, endDate);

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитаны повторения!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Fast)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(RecurringSettings.CalculateOccurrences)} при установке цикла.")]
        public void CalculateOccurrences_SetСycle_ReturnsCorrectData()
        {
            var startDate = new DateTime(2025, 3, 1, 12, 30, 0);
            var endDate = new DateTime(2025, 5, 1, 12, 30, 0);
            var cycle = new RecurringCycle()
            {
                Minutes = 1UL << 0 | 1UL << 50,
                Hours = 1U << 12,
                WeekDays = WeekDay.Saturday,
                MonthDays = 1U << 0 | 1U << 4 | 1U << 14,
                Months = Month.March
            };
            var frequency = TimeSpan.FromDays(7);
            var expected = new DateTime[]
            {
                new DateTime(2025, 3, 1, 12, 50, 0),
                new DateTime(2025, 3, 15, 12, 0, 0),
                new DateTime(2025, 3, 15, 12, 50, 0)
            };

            _settings.Frequency = frequency;
            _settings.Cycle = cycle;
            var result = _settings.CalculateOccurrences(startDate, endDate);

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитаны повторения!");
        }
    }
}
