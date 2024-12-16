using ViewModel.Implementations;

namespace ViewModel.Tests
{
    [TestFixture(Category = "Unit", TestOf = typeof(TimeScheduler),
        Description = $"Тестирование класса {nameof(TimeScheduler)}.")]
    public class TimeSchedulerTests
    {
        private TimeScheduler _timeScheduler;

        [SetUp]
        public void Setup()
        {
            _timeScheduler = new();
        }

        [Test(Description = $"Тестирование свойства {nameof(TimeScheduler.Timepoints)}" +
            "при инициализации.")]
        public void GetTimepoints_InitialState_TimepointsEmpty()
        {
            var result = _timeScheduler.Timepoints;

            Assert.That(result, Is.Empty, "Список временных точек должен быть пустым!");
        }

        [Test(Description = $"Тестирование свойства {nameof(TimeScheduler.Timepoints)}" +
            "при добавлении временной точки из прошлого.")]
        public void GetTimepoints_AddPastTimepoint_TimepointsEmpty()
        {
            var addingTimepoint = DateTime.Now.AddSeconds(-10);

            _timeScheduler.Timepoints.Add(addingTimepoint);
            var result = _timeScheduler.Timepoints;

            Assert.That(result, Is.Empty, "Список временных точек должен быть пустым!");
        }

        [Test(Description = $"Тестирование свойства {nameof(TimeScheduler.Timepoints)}" +
            "при добавлении временной точки из будущего.")]
        public void GetTimepoints_AddFutureTimepoint_TimepointsEmpty()
        {
            var addingTimepoint = DateTime.Now.AddSeconds(10);
            var expected = new DateTime[] { addingTimepoint };

            _timeScheduler.Timepoints.Add(addingTimepoint);
            var result = _timeScheduler.Timepoints;

            Assert.That(result, Is.EqualTo(expected),
                "Список временных точек должен состоять из одной добавленной точки!");
        }

        [Retry(5)]
        [TestCase([1000, 100])]
        [TestCase([100, 10])]
        [TestCase([10, 1])]
        [TestCase([1, 0])]
        [Test(Description = $"Тестирование события {nameof(TimeScheduler.Timepoints)}" +
            "при добавлении временной точки из прошлого и ожидании времени.")]
        public async Task EventTimepointReached_AddFutureTimepointWithWait_InvokeEventHandler
            (int time, int toleranceTime)
        {
            var timepoint = DateTime.Now.AddMilliseconds(time);

            var result = false;
            _timeScheduler.TimepointReached += (sender, t) =>
            {
                result = true;
            };
            _timeScheduler.Timepoints.Add(timepoint);
            await Task.Delay(time + toleranceTime);

            Assert.That(result, "Должно отработать событие!");
        }

        [Retry(5)]
        [TestCase([1000, 100])]
        [TestCase([100, 10])]
        [TestCase([10, 1])]
        [TestCase([1, 0])]
        [Test(Description = $"Тестирование события {nameof(TimeScheduler.Timepoints)}" +
            "при добавлении временной точки и удалении её, ожидании времени.")]
        public async Task EventTimepointReached_AddFutureTimepointWithRemoveAndWait_NoInvokeEventHandler
            (int time, int toleranceTime)
        {
            var timepoint = DateTime.Now.AddMilliseconds(time);

            var result = false;
            _timeScheduler.TimepointReached += (sender, t) =>
            {
                result = true;
            };
            _timeScheduler.Timepoints.Add(timepoint);
            _timeScheduler.Timepoints.Remove(timepoint);
            await Task.Delay(time + toleranceTime);

            Assert.That(result, Is.False, "Не должно отработать событие!");
        }

        [Retry(5)]
        [TestCase([1000, 2000, 100])]
        [TestCase([100, 200, 10])]
        [TestCase([10, 20, 1])]
        [TestCase([1, 2, 0])]
        [Test(Description = $"Тестирование события {nameof(TimeScheduler.Timepoints)}" +
            "при добавлении временных точек по порядку и ожидании времени.")]
        public async Task EventTimepointReached_AddFuture2TimepointInOrderWithWait_2InvokeEventHandlerInOrder
            (int firstTime, int secondTime, int toleranceTime)
        {
            var firstTimepoint = DateTime.Now.AddMilliseconds(firstTime);
            var secondTimepoint = DateTime.Now.AddMilliseconds(secondTime);
            var firstExpected = new DateTime[] { firstTimepoint };
            var secondExpected = new DateTime[] { firstTimepoint, secondTimepoint };
            var result = new List<DateTime>();

            _timeScheduler.TimepointReached += (sender, t) =>
            {
                result.Add(t);
            };
            _timeScheduler.Timepoints.Add(firstTimepoint);
            _timeScheduler.Timepoints.Add(secondTimepoint);

            await Assert.MultipleAsync(async () =>
            {
                await Task.Delay(firstTime + toleranceTime);

                Assert.That(result, Is.EqualTo(firstExpected),
                    "Должно верно отработать событие в первом случае!");

                await Task.Delay(secondTime - firstTime + toleranceTime);

                Assert.That(result, Is.EqualTo(secondExpected),
                    "Должно верно отработать событие во втором случае!");
            });
        }

        [Retry(5)]
        [TestCase([2000, 1000, 100])]
        [TestCase([200, 100, 10])]
        [TestCase([20, 10, 1])]
        [TestCase([2, 1, 0])]
        [Test(Description = $"Тестирование события {nameof(TimeScheduler.Timepoints)}" +
            "при добавлении временных точек не по порядку и ожидании времени.")]
        public async Task EventTimepointReached_AddFuture2TimepointNotInOrderWithWait_2InvokeEventHandlerInOrder
            (int firstTime, int secondTime, int toleranceTime)
        {
            var firstTimepoint = DateTime.Now.AddMilliseconds(firstTime);
            var secondTimepoint = DateTime.Now.AddMilliseconds(secondTime);
            var firstExpected = new DateTime[] { secondTimepoint };
            var secondExpected = new DateTime[] { secondTimepoint, firstTimepoint };
            var result = new List<DateTime>();

            _timeScheduler.TimepointReached += (sender, t) =>
            {
                result.Add(t);
            };
            _timeScheduler.Timepoints.Add(firstTimepoint);
            _timeScheduler.Timepoints.Add(secondTimepoint);

            await Assert.MultipleAsync(async () =>
            {
                await Task.Delay(secondTime + toleranceTime);

                Assert.That(result, Is.EqualTo(firstExpected),
                    "Должно верно отработать событие в первом случае!");

                await Task.Delay(firstTime - secondTime + toleranceTime);

                Assert.That(result, Is.EqualTo(secondExpected),
                    "Должно верно отработать событие во втором случае!");
            });
        }
    }
}
