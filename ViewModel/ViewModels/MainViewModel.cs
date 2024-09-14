using ViewModel.ViewModels.Pages;

namespace ViewModel.ViewModels;

public class MainViewModel : ViewModelBase
{
    private IEnumerable<PageViewModel> _pages;

    private PageViewModel? _selectedPage;

    public IEnumerable<PageViewModel> Pages
    {
        get => _pages;
        set
        {
            if (UpdateProperty(ref _pages, value))
            {
                SelectedPage = Pages.FirstOrDefault();
            }
        }
    }

    public PageViewModel? SelectedPage
    {
        get => _selectedPage;
        set => UpdateProperty(ref _selectedPage, value);
    }

    public MainViewModel(IEnumerable<PageViewModel> pages)
    {
        Pages = pages;
    }

    public MainViewModel() : this([new TaskEditorViewModel(), new TaskViewModel()]) { }
}
