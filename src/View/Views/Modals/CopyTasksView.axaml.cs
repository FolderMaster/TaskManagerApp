using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// ����� ����������������� �������� ������� ����������� �����.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{CopyTasksViewModel}"/>.
/// </remarks>
public partial class CopyTasksView : ReactiveUserControl<CopyTasksViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="CopyTasksView"/> �� ���������.
    /// </summary>
    public CopyTasksView()
    {
        InitializeComponent();
    }
}