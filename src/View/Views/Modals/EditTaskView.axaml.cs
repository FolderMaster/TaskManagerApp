using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// ����� ����������������� �������� ������� ��������� ������.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{EditTaskViewModel}"/>.
/// </remarks>
public partial class EditTaskView : ReactiveUserControl<EditTaskViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="EditTaskView"/> �� ���������.
    /// </summary>
    public EditTaskView()
    {
        InitializeComponent();
    }
}