using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

public partial class RemoveTasksView : ReactiveUserControl<RemoveTasksViewModel>
{
    public RemoveTasksView()
    {
        InitializeComponent();
    }
}