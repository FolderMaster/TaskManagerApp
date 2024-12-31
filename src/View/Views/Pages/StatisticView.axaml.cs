using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// ����� ����������������� �������� �������� ����������.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{StatisticViewModel}"/>.
/// </remarks>
public partial class StatisticView : ReactiveUserControl<StatisticViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="StatisticView"/> �� ���������.
    /// </summary>
    public StatisticView()
    {
        InitializeComponent();
    }
}