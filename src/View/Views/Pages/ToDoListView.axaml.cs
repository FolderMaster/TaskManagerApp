using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// ����� ����������������� �������� �������� ������ ����� ��� ����������.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{ToDoListViewModel}"/>.
/// </remarks>
public partial class ToDoListView : ReactiveUserControl<ToDoListViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="ToDoListView"/> �� ���������.
    /// </summary>
    public ToDoListView()
    {
        InitializeComponent();
    }
}