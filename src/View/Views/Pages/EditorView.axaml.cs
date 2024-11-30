using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

public partial class EditorView : ReactiveUserControl<EditorViewModel>
{
    public EditorView()
    {
        InitializeComponent();
    }       
}