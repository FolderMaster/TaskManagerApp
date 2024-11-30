using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

public partial class AddTaskView : ReactiveUserControl<AddTaskViewModel>
{
    public AddTaskView()
    {
        InitializeComponent();
    }
}