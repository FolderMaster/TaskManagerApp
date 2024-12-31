using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Modals;

namespace View.Views.Modals;

/// <summary>
/// ����� ����������������� �������� ������� ���������� ���������� ���������.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{AddTimeIntervalViewModel}"/>.
/// </remarks>
public partial class AddTimeIntervalView : ReactiveUserControl<AddTimeIntervalViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="AddTimeIntervalView"/> �� ���������.
    /// </summary>
    public AddTimeIntervalView()
    {
        InitializeComponent();
    }
}