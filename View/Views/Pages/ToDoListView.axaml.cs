using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

public partial class ToDoListView : ReactiveUserControl<ToDoListViewModel>
{
    public ToDoListView()
    {
        InitializeComponent();
    }
}