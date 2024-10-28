using Model;

namespace ViewModel.ViewModels.Pages
{
    public partial class StatisticViewModel : PageViewModel
    {
        private IList<ITask> _mainTaskList;

        public StatisticViewModel(object metadata, IList<ITask> mainTaskList) : base(metadata)
        {
            _mainTaskList = mainTaskList;
        }
    }
}
