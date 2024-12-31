using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// ����� ����������������� �������� ������� ���������� ������.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{AddTaskViewModel}"/>.
/// </remarks>
public partial class AddTaskView : ReactiveUserControl<AddTaskViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="AddTaskView"/> �� ���������.
    /// </summary>
    public AddTaskView()
    {
        InitializeComponent();
    }
}