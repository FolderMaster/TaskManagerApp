using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

public partial class MoveTasksView : ReactiveUserControl<MoveTasksViewModel>
{
    public MoveTasksView()
    {
        InitializeComponent();
    }
}