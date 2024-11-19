using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

public partial class EditTaskView : ReactiveUserControl<EditTaskViewModel>
{
    public EditTaskView()
    {
        InitializeComponent();
    }
}