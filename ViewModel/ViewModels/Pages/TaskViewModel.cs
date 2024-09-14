using System.Collections;

using Model;

namespace ViewModel.ViewModels.Pages;

public class TaskViewModel : PageViewModel
{
    public TaskViewModel(object metadata) : base(metadata) { }

    public TaskViewModel() : this("Task") { }
}
