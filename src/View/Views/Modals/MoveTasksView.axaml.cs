using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// ����� ����������������� �������� ������� ����������� �����.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{MoveTasksViewModel}"/>.
/// </remarks>
public partial class MoveTasksView : ReactiveUserControl<MoveTasksViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="MoveTasksView"/> �� ���������.
    /// </summary>
    public MoveTasksView()
    {
        InitializeComponent();
    }
}