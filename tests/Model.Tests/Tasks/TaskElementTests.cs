using Model.Tasks;

namespace Model.Tests.Tasks
{
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
    }
}
