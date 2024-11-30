using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

public partial class StatisticView : ReactiveUserControl<StatisticViewModel>
{
    public StatisticView()
    {
        InitializeComponent();
    }
}