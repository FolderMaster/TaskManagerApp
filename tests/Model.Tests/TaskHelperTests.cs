using Common.Tests;
using Model.Interfaces;
using Model.Tasks;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace Model.Tests
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(TaskHelper),
        Description = $"Тестирование класса {nameof(TaskHelper)}.")]
    public class TaskHelperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Severity(SeverityLevel.Major)]
        [Priority(PriorityLevel.High)]
        [Test(Description = $"Тестирование метода {nameof(TaskHelper.IsTaskCompleted)}")]
        public void IsTaskCompleted_ReturnCorrectValues()
        {
            var statuses = new TaskStatus[]
            {
                TaskStatus.Cancelled,
                TaskStatus.Deferred,
                TaskStatus.Planned,
                TaskStatus.Closed,
                TaskStatus.Blocked,
                TaskStatus.OnHold,
                TaskStatus.InProgress
            };
            var task = new TaskElement();
            var expected = new bool[] { true, true, true, true, false, false, false };

            var result = new List<bool>();
            foreach (var status in statuses)
            {
                task.Status = status;
                result.Add(TaskHelper.IsTaskCompleted(task));
            }

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно определена завершенность задач!");
        }

        [Severity(SeverityLevel.Major)]
        [Priority(PriorityLevel.High)]
        [Test(Description = $"Тестирование метода {nameof(TaskHelper.IsTaskCompleted)} " +
            "при задаче без срока.")]
        public void HasTaskExpired_TaskWithoutDeadline_ReturnFalse()
        {
            var task = new TaskElement() { Deadline = null };
            var expected = false;

            var result = TaskHelper.HasTaskExpired(task);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно определено истекание срока задачи!");
        }

        [Severity(SeverityLevel.Major)]
        [Priority(PriorityLevel.High)]
        [Test(Description = $"Тестирование метода {nameof(TaskHelper.IsTaskCompleted)}" +
            "при задаче со сроком, которое превосходит дополнительное время.")]
        public void HasTaskExpired_TaskWithDeadlineMoreWarningTime_ReturnFalse()
        {
            var task = new TaskElement() { Deadline = DateTime.Now.AddDays(1) };
            var warningTime = new TimeSpan(1, 0, 0);
            var expected = false;

            var result = TaskHelper.HasTaskExpired(task, warningTime);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно определено истекание срока задачи!");
        }

        [Severity(SeverityLevel.Major)]
        [Priority(PriorityLevel.High)]
        [Test(Description = $"Тестирование метода {nameof(TaskHelper.IsTaskCompleted)}" +
            "при задаче со сроком, которое превосходит дополнительное время.")]
        public void HasTaskExpired_TaskWithDeadlineMoreWarningTime_ReturnTrue()
        {
            var task = new TaskElement() { Deadline = DateTime.Now.AddDays(1) };
            var warningTime = new TimeSpan(2, 0, 0, 0);
            var expected = true;

            var result = TaskHelper.HasTaskExpired(task, warningTime);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно определено истекание срока задачи!");
        }

        [Severity(SeverityLevel.Critical)]
        [Priority(PriorityLevel.High)]
        [Test(Description = $"Тестирование метода {nameof(TaskHelper.GetTasks)}.")]
        public void GetTasks_ReturnCorrectData()
        {
            var taskElement1 = new TaskElement();
            var taskElement2 = new TaskElement();
            var taskCoposite1 = new TaskComposite([ taskElement2 ]);
            var taskElement3 = new TaskElement();
            var taskCoposite2 = new TaskComposite([ taskElement3, taskCoposite1 ]);
            var tasks = new ITask[]
            {
                taskElement1,
                taskCoposite2
            };
            var expected = new ITask[]
            {
                taskElement1,
                taskCoposite2,
                taskElement3,
                taskCoposite1,
                taskElement2
            };

            var result = TaskHelper.GetTasks(tasks);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно определено истекание срока задачи!");
        }

        [Severity(SeverityLevel.Critical)]
        [Priority(PriorityLevel.High)]
        [Test(Description = $"Тестирование метода {nameof(TaskHelper.GetTaskElements)}.")]
        public void GetTaskElements_ReturnCorrectData()
        {
            var taskElement1 = new TaskElement();
            var taskElement2 = new TaskElement();
            var taskCoposite1 = new TaskComposite([taskElement2]);
            var taskElement3 = new TaskElement();
            var taskCoposite2 = new TaskComposite([taskElement3, taskCoposite1]);
            var tasks = new ITask[]
            {
                taskElement1,
                taskCoposite2
            };
            var expected = new ITask[]
            {
                taskElement1,
                taskElement3,
                taskElement2
            };

            var result = TaskHelper.GetTaskElements(tasks);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно определено истекание срока задачи!");
        }

        [Severity(SeverityLevel.Critical)]
        [Priority(PriorityLevel.High)]
        [Test(Description = $"Тестирование метода {nameof(TaskHelper.GetTaskComposites)}.")]
        public void GetTaskComposites_ReturnCorrectData()
        {
            var taskElement1 = new TaskElement();
            var taskElement2 = new TaskElement();
            var taskCoposite1 = new TaskComposite([taskElement2]);
            var taskElement3 = new TaskElement();
            var taskCoposite2 = new TaskComposite([taskElement3, taskCoposite1]);
            var tasks = new ITask[]
            {
                taskElement1,
                taskCoposite2
            };
            var expected = new ITask[]
            {
                taskCoposite2,
                taskCoposite1
            };

            var result = TaskHelper.GetTaskComposites(tasks);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно определено истекание срока задачи!");
        }
    }
}
