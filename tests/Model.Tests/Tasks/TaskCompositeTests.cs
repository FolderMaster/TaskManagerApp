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
    [TestFixture(TestOf = typeof(TaskComposite),
        Description = $"“естирование класса {nameof(TaskComposite)}.")]
    public class TaskCompositeTests
    {
        private TaskComposite _taskComposite;

        [SetUp]
        public void Setup()
        {
            _taskComposite = new();
        }

        [Test(Description = $"“естирование свойства {nameof(TaskComposite.Difficult)} " +
            "при добавлении задач.")]
        public void GetDifficult_AddTaskElements_ReturnMaxDifficult()
        {
            var taskElement1 = new TaskElement() { Difficult = 1 };
            var taskElement2 = new TaskElement() { Difficult = 2 };
            var expected = 2;

            _taskComposite.Add(taskElement1);
            _taskComposite.Add(taskElement2);
            var result = _taskComposite.Difficult;

            Assert.That(result, Is.EqualTo(expected), "Ќеправильно расчитана сложность!");
        }

        [Test(Description = $"“естирование свойства {nameof(TaskComposite.Priority)} " +
            "при добавлении задач.")]
        public void GetPriority_AddTaskElements_ReturnMaxPriority()
        {
            var taskElement1 = new TaskElement() { Priority = 1 };
            var taskElement2 = new TaskElement() { Priority = 2 };
            var expected = 2;

            _taskComposite.Add(taskElement1);
            _taskComposite.Add(taskElement2);
            var result = _taskComposite.Priority;

            Assert.That(result, Is.EqualTo(expected), "Ќеправильно расчитан приоритет!");
        }

        [Test(Description = $"“естирование свойства {nameof(TaskComposite.Progress)} " +
            "при добавлении задач.")]
        public void GetProgress_AddTaskElements_ReturnAverageProgress()
        {
            var taskElement1 = new TaskElement();
            taskElement1.Execution.Progress = 1;
            var taskElement2 = new TaskElement();
            taskElement2.Execution.Progress = 0;
            var expected = 0.5;

            _taskComposite.Add(taskElement1);
            _taskComposite.Add(taskElement2);
            var result = _taskComposite.Progress;

            Assert.That(result, Is.EqualTo(expected), "Ќеправильно расчитан прогресс!");
        }

        [Test(Description = $"“естирование свойства {nameof(TaskComposite.Status)} " +
            "при добавлении задач.")]
        public void GetStatus_AddTaskElements_ReturnMinStatus()
        {
            var taskElement1 = new TaskElement();
            taskElement1.Execution.Status = TaskStatus.InProgress;
            var taskElement2 = new TaskElement();
            taskElement2.Execution.Status = TaskStatus.Planned;
            var expected = TaskStatus.InProgress;

            _taskComposite.Add(taskElement1);
            _taskComposite.Add(taskElement2);
            var result = _taskComposite.Status;

            Assert.That(result, Is.EqualTo(expected), "Ќеправильно расчитан статус!");
        }

        [Test(Description = $"“естирование свойства {nameof(TaskComposite.Deadline)} " +
            "при добавлении задач.")]
        public void GetDeadline_AddTaskElements_ReturnMaxDeadline()
        {
            var taskElement1 = new TaskElement() { Deadline = new DateTime(2024, 12, 1) };
            var taskElement2 = new TaskElement() { Deadline = null };
            var expected = new DateTime(2024, 12, 1);

            _taskComposite.Add(taskElement1);
            _taskComposite.Add(taskElement2);
            var result = _taskComposite.Deadline;

            Assert.That(result, Is.EqualTo(expected), "Ќеправильно расчитан срок!");
        }

        [Test(Description = $"“естирование свойства {nameof(TaskComposite.PlannedTime)} " +
            "при добавлении задач.")]
        public void GetPlannedTime_AddTaskElements_ReturnSumPlannedTime()
        {
            var taskElement1 = new TaskElement() { PlannedTime = new TimeSpan(2, 20, 0) };
            var taskElement2 = new TaskElement() { PlannedTime = new TimeSpan(1, 40, 0) };
            var expected = new TimeSpan(4, 0, 0);

            _taskComposite.Add(taskElement1);
            _taskComposite.Add(taskElement2);
            var result = _taskComposite.PlannedTime;

            Assert.That(result, Is.EqualTo(expected),
                "Ќеправильно расчитан запланированное врем€!");
        }

        [Test(Description = $"“естирование свойства {nameof(TaskComposite.SpentTime)} " +
            "при добавлении задач.")]
        public void GetSpentTime_AddTaskElements_ReturnSumSpentTime()
        {
            var taskElement1 = new TaskElement();
            taskElement1.Execution.SpentTime = new TimeSpan(2, 20, 0);
            var taskElement2 = new TaskElement();
            taskElement2.Execution.SpentTime = new TimeSpan(1, 40, 0);
            var expected = new TimeSpan(4, 0, 0);

            _taskComposite.Add(taskElement1);
            _taskComposite.Add(taskElement2);
            var result = _taskComposite.SpentTime;

            Assert.That(result, Is.EqualTo(expected), "Ќеправильно расчитан потраченное врем€!");
        }
    }
}