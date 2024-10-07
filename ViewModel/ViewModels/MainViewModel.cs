using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using ViewModel.ViewModels.Pages;

namespace ViewModel.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [Reactive]
    private IEnumerable<PageViewModel> _pages;

    [Reactive]
    private PageViewModel? _selectedPage;

    public MainViewModel(IEnumerable<PageViewModel> pages)
    {
        Pages = pages;

        this.WhenAnyValue(x => x.Pages).Subscribe
            (p => SelectedPage = p != null ? p.FirstOrDefault() : null);
    }

    public MainViewModel() : this([new TaskEditorViewModel(), new TaskViewModel()]) { }
}
