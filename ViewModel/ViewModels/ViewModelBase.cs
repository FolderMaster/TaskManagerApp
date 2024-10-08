using System.Collections.ObjectModel;
using ReactiveUI;

namespace ViewModel.ViewModels;

public partial class ViewModelBase : ReactiveObject
{
    private ObservableCollection<DialogViewModel> _dialogs = new();

    private ObservableCollection<DialogViewModel> _modals = new();

    public ObservableCollection<DialogViewModel> Dialogs => _dialogs;

    public ObservableCollection<DialogViewModel> Modals => _modals;

    public async Task<object?> AddDialog(DialogViewModel dialog)
    {
        _dialogs.Add(dialog);
        var result = await dialog.Invoke(this);
        _dialogs.Remove(dialog);
        return result;
    }

    public async Task<object?> AddModal(DialogViewModel modal)
    {
        _modals.Add(modal);
        var result = await AddDialog(modal);
        _modals.Remove(modal);
        return result;
    }
}
