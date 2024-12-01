using System.Collections.ObjectModel;

using TrackableFeatures;

using Model.Interfaces;

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
