namespace ViewModel.ViewModels;

public class PageViewModel : ViewModelBase
{
    public object Metadata { get; private set; }

    public PageViewModel(object metadata) => Metadata = metadata;
}
