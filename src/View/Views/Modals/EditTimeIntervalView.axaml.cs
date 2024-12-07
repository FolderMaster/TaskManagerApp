using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

public partial class EditTimeIntervalView : ReactiveUserControl<EditTimeIntervalViewModel>
{
    public EditTimeIntervalView()
    {
        InitializeComponent();
    }
}