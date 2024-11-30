using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

public partial class AddTimeIntervalView : ReactiveUserControl<AddTimeIntervalViewModel>
{
    public AddTimeIntervalView()
    {
        InitializeComponent();
    }
}