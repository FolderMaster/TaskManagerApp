using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

public partial class TimeView : ReactiveUserControl<TimeViewModel>
{
    public TimeView()
    {
        InitializeComponent();
    }
}