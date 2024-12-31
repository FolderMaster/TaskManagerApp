using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// ����� ����������������� �������� ������� ��������� ���������� ���������.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{EditTimeIntervalViewModel}"/>.
/// </remarks>
public partial class EditTimeIntervalView : ReactiveUserControl<EditTimeIntervalViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="EditTimeIntervalView"/> �� ���������.
    /// </summary>
    public EditTimeIntervalView()
    {
        InitializeComponent();
    }
}