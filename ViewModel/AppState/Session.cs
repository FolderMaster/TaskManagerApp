using System.Collections.ObjectModel;

using Model;

namespace ViewModel.AppState
{
    public class Session : TrackableObject
    {
        private IList<ITask>? _tasks = new ObservableCollection<ITask>();

        public IList<ITask>? Tasks
        {
            get => _tasks;
            set => UpdateProperty(ref _tasks, value);
        }
    }
}
