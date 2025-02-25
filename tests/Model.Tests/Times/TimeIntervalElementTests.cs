using Common.Tests;
using Model.Times;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace Model.Tests.Times
{
    [Level(TestLevel.Unit)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Major)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(TimeIntervalElement),
        Description = $"Тестирование класса {nameof(TimeIntervalElement)}.")]
    public class TimeIntervalElementTests
    {
        private TimeIntervalElement _timeIntervalElement;

        [SetUp]
        public void Setup()
        {
            _timeIntervalElement = new();
        }

        [Test(Description = $"Тестирование свойства {nameof(TimeIntervalElement.Duration)} " +
            "при установки начала и конца временного интервала.")]
        public void GetDuration_SetStartAndEnd_ReturnDifferenceStartAndEnd()
        {
            var start = new DateTime(2024, 12, 1);
            var end = new DateTime(2024, 12, 2);
            var expected = new TimeSpan(1, 0, 0, 0);

            _timeIntervalElement.Start = start;
            _timeIntervalElement.End = end;
            var result = _timeIntervalElement.Duration;

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитана длительность!");
        }
    }
}
