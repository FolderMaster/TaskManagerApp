using Model;

namespace ViewModel.ViewModels.Pages;

public class TimeViewModel : PageViewModel
{
    private IList<ITask> _mainTaskList;

    public TimeViewModel(object metadata, IList<ITask> mainTaskList) : base(metadata)
    {
        _mainTaskList = mainTaskList;
    }
}
