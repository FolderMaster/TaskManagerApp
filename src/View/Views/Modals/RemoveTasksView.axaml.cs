using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// ����� ����������������� �������� ������� �������� �����.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{RemoveTasksViewModel}"/>.
/// </remarks>
public partial class RemoveTasksView : ReactiveUserControl<RemoveTasksViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="RemoveTasksView"/> �� ���������.
    /// </summary>
    public RemoveTasksView()
    {
        InitializeComponent();
    }
}