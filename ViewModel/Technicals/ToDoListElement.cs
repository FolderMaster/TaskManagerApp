using Model;

namespace ViewModel.Technicals
{
    public class ToDoListElement
    {
        public ITaskElement TaskElement { get; private set; }

        public bool IsLagging { get; private set; }

        public bool IsExpired { get; private set; }

        public ToDoListElement(ITaskElement taskElement, bool isLagging, bool isExpired)
        {
            TaskElement = taskElement;
            IsLagging = isLagging;
            IsExpired = isExpired;
        }
    }
}
