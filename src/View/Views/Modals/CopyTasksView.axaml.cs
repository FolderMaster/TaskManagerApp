using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

public partial class CopyTasksView : ReactiveUserControl<CopyTasksViewModel>
{
    public CopyTasksView()
    {
        InitializeComponent();
    }
}