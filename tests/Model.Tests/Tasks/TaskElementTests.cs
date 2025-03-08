using Common.Tests;

using Model.Tasks;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace Model.Tests.Tasks
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Minor)]
    [Priority(PriorityLevel.Low)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(TaskElement),
        Description = $"Тестирование класса {nameof(TaskElement)}.")]
    public class TaskElementTests
    {
        private TaskElement _taskElement;

        [SetUp]
        public void Setup()
        {
            _taskElement = new();
        }

        [Test(Description = $"Тестирование свойства {nameof(TaskElement.Progress)} " +
            "при установки прогресса в выполнении.")]
        public void GetProgress_SetExecutionProgress_ReturnsExecutionProgress()
        {
            var progress = 1;

            _taskElement.Execution.Progress = progress;
            var result = _taskElement.Progress;

            Assert.That(result, Is.EqualTo(progress), "Неправильно расчитан прогресс!");
        }

        [Test(Description = $"Тестирование свойства {nameof(TaskElement.Status)} " +
            "при установки статуса в выполнении.")]
        public void GetStatus_SetExecutionStatus_ReturnsExecutionStatus()
        {
            var status = TaskStatus.Closed;

            _taskElement.Execution.Status = status;
            var result = _taskElement.Status;

            Assert.That(result, Is.EqualTo(status), "Неправильно расчитан статус!");
        }

        [Test(Description = $"Тестирование свойства {nameof(TaskElement.SpentTime)} " +
            "при установки проведённого времени в выполнении.")]
        public void GetSpentTime_SetExecutionSpentTime_ReturnsExecutionSpentTime()
        {
            var spentTime = new TimeSpan(1, 0, 0);

            _taskElement.Execution.SpentTime = spentTime;
            var result = _taskElement.SpentTime;

            Assert.That(result, Is.EqualTo(spentTime), "Неправильно расчитан проведённое время!");
        }

        [Test(Description = $"Тестирование свойства {nameof(TaskElement.ExecutedReal)} " +
            "при установки выполненного реального показателя в выполнении.")]
        public void GetExecutedReal_SetExecutionExecutedReal_ReturnsExecutionExecutedReal()
        {
            var executedReal = 10;

            _taskElement.Execution.ExecutedReal = executedReal;
            var result = _taskElement.ExecutedReal;

            Assert.That(result, Is.EqualTo(executedReal),
                "Неправильно расчитан выполненнный реальный показатель!");
        }
    }
}
