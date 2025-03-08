using Common.Tests;

using Model.Tasks;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace Model.Tests.Tasks
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Reproducibility(ReproducibilityType.Stable)]
    [TestFixture(TestOf = typeof(RecurringTaskElement),
        Description = $"Тестирование класса {nameof(RecurringTaskElement)}.")]
    public class RecurringTaskElementTests
    {
        private RecurringTaskElement _recurringTaskElement;

        [SetUp]
        public void Setup()
        {
            _recurringTaskElement = new();
        }

        [Severity(SeverityLevel.Major)]
        [Priority(PriorityLevel.High)]
        [Time(TestTime.Fast)]
        [Test(Description = $"Тестирование свойства {nameof(RecurringTaskElement.ExecutedReal)} " +
            "при добавлении выполнений.")]
        public void UpdateExecutions_SetRecurringSettingsProperties_NewExecutionsAdded()
        {
            var now = DateTime.Now;
            var startDate = now.AddDays(-10);
            var lastUpdatedExecutionsDate = now.AddDays(-7);
            var frequency = TimeSpan.FromDays(3);
            var expected = 3;

            _recurringTaskElement._lastUpdatedExecutionsDate = lastUpdatedExecutionsDate;
            _recurringTaskElement.RecurringSettings.StartDate = startDate;
            _recurringTaskElement.RecurringSettings.Frequency = frequency;
            _recurringTaskElement.UpdateExecutions();
            var result = _recurringTaskElement.Executions.Count();

            Assert.That(result, Is.EqualTo(expected), "Неправильно обновлены выполнения!");
        }

        [Severity(SeverityLevel.Minor)]
        [Priority(PriorityLevel.Low)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование свойства {nameof(RecurringTaskElement.Progress)} " +
            "при добавлении выполнений.")]
        public void GetProgress_AddTaskElementExecutions_ReturnAverageProgress()
        {
            var taskElementExecution1 = new TaskElementExecution();
            taskElementExecution1.Progress = 1;
            var taskElementExecution2 = new TaskElementExecution();
            taskElementExecution2.Progress = 0;
            var expected = 0.5;

            _recurringTaskElement._executions.Add(taskElementExecution1);
            _recurringTaskElement._executions.Add(taskElementExecution2);
            var result = _recurringTaskElement.Progress;

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитан прогресс!");
        }

        [Severity(SeverityLevel.Minor)]
        [Priority(PriorityLevel.Low)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование свойства {nameof(RecurringTaskElement.Status)} " +
            "при добавлении выполнений.")]
        public void GetStatus_AddTaskElementExecutions_ReturnMinStatus()
        {
            var taskElementExecution1 = new TaskElementExecution();
            taskElementExecution1.Status = TaskStatus.InProgress;
            var taskElementExecution2 = new TaskElementExecution();
            taskElementExecution2.Status = TaskStatus.Planned;
            var expected = TaskStatus.InProgress;

            _recurringTaskElement._executions.Add(taskElementExecution1);
            _recurringTaskElement._executions.Add(taskElementExecution2);
            var result = _recurringTaskElement.Status;

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитан статус!");
        }

        [Severity(SeverityLevel.Minor)]
        [Priority(PriorityLevel.Low)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование свойства {nameof(RecurringTaskElement.SpentTime)} " +
            "при добавлении выполнений.")]
        public void GetSpentTime_AddTaskElementExecutions_ReturnSumSpentTime()
        {
            var taskElementExecution1 = new TaskElementExecution();
            taskElementExecution1.SpentTime = new TimeSpan(2, 20, 0);
            var taskElementExecution2 = new TaskElementExecution();
            taskElementExecution2.SpentTime = new TimeSpan(1, 40, 0);
            var expected = new TimeSpan(4, 0, 0);

            _recurringTaskElement._executions.Add(taskElementExecution1);
            _recurringTaskElement._executions.Add(taskElementExecution2);
            var result = _recurringTaskElement.SpentTime;

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитан потраченное время!");
        }

        [Severity(SeverityLevel.Minor)]
        [Priority(PriorityLevel.Low)]
        [Time(TestTime.Instant)]
        [Test(Description = $"Тестирование свойства {nameof(RecurringTaskElement.ExecutedReal)} " +
            "при добавлении выполнений.")]
        public void GetExecutedReal_AddTaskElementExecutions_ReturnsAverageExecutedReal()
        {
            var taskElementExecution1 = new TaskElementExecution();
            taskElementExecution1.ExecutedReal = 10;
            var taskElementExecution2 = new TaskElementExecution();
            taskElementExecution2.ExecutedReal = 20;
            var expected = 15;

            _recurringTaskElement._executions.Add(taskElementExecution1);
            _recurringTaskElement._executions.Add(taskElementExecution2);
            var result = _recurringTaskElement.ExecutedReal;

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно расчитан выполненный реальный показатель!");
        }
    }
}
