using System.Collections.ObjectModel;
using Model.Interfaces;
using Model.Technicals;

namespace ViewModel.AppStates
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
