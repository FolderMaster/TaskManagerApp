using Avalonia.ReactiveUI;

using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// ����� ����������������� �������� �������� ��������� �����.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{EditorViewModel}"/>.
/// </remarks>
public partial class EditorView : ReactiveUserControl<EditorViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="EditorView"/> �� ���������.
    /// </summary>
    public EditorView()
    {
        InitializeComponent();
    }       
}