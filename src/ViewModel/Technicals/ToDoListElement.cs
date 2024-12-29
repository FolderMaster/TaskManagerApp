using Model.Interfaces;

namespace ViewModel.Technicals
{
    public class ToDoListElement
    {
        public ITaskElement TaskElement { get; private set; }

        public double? ExecutionChance { get; private set; }

        public bool IsLagging { get; private set; }

        public bool IsExpired { get; private set; }

        public ToDoListElement(ITaskElement taskElement,
            double? executionChance, bool isLagging, bool isExpired)
        {
            TaskElement = taskElement;
            ExecutionChance = executionChance;
            IsLagging = isLagging;
            IsExpired = isExpired;
        }
    }
}
