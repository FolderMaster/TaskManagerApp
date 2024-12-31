using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// ����� ����������������� �������� �������� ��������.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{SettingsViewModel}"/>.
/// </remarks>
public partial class SettingsView : ReactiveUserControl<SettingsViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="SettingsView"/> �� ���������.
    /// </summary>
    public SettingsView()
    {
        InitializeComponent();
    }
}