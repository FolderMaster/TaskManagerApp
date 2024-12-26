using Model.Times;

namespace Model.Tests.Times
{
    [TestFixture(Category = "Unit", TestOf = typeof(TimeIntervalList),
        Description = $"Тестирование класса {nameof(TimeIntervalList)}.")]
    public class TimeIntervalListTests
    {
        private TimeIntervalList _timeIntervalList;

        [SetUp]
        public void Setup()
        {
            _timeIntervalList = new();
        }

        [Test(Description = $"Тестирование свойства {nameof(TimeIntervalList.Duration)} " +
            "при добавлении временных интервалов.")]
        public void GetDuration_AddTimeIntervalElements_ReturnSumDuration()
        {
            var timeIntervalElement1 = new TimeIntervalElement
                (new DateTime(2024, 12, 1), new DateTime(2024, 12, 2));
            var timeIntervalElement2 = new TimeIntervalElement
                (new DateTime(2024, 12, 3), new DateTime(2024, 12, 4));
            var expected = new TimeSpan(2, 0, 0, 0);

            _timeIntervalList.Add(timeIntervalElement1);
            _timeIntervalList.Add(timeIntervalElement2);
            var result = _timeIntervalList.Duration;

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитана длительность!");
        }
    }
}
