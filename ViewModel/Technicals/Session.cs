using System.Collections.ObjectModel;

using Model;

namespace ViewModel.Technicals
{
    public class Session : ObservableObject
    {
        private IList<ITask>? _tasks = new ObservableCollection<ITask>();

        public IList<ITask>? Tasks
        {
            get => _tasks;
            set => UpdateProperty(ref _tasks, value);
        }
    }
}
