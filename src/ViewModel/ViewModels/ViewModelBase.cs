using System.Collections.ObjectModel;
using ReactiveUI;

namespace ViewModel.ViewModels;

public partial class ViewModelBase : ReactiveObject, IActivatableViewModel
{
    private ObservableCollection<ViewModelBase> _dialogs = new();

    private ObservableCollection<ViewModelBase> _modals = new();

    public ObservableCollection<ViewModelBase> Dialogs => _dialogs;

    public ObservableCollection<ViewModelBase> Modals => _modals;

    public ViewModelActivator Activator { get; protected set; }

    public async Task<R> AddDialog<A, R>(DialogViewModel<A, R> dialog, A args)
    {
        _dialogs.Add(dialog);
        var result = await dialog.Invoke(this, args);
        _dialogs.Remove(dialog);
        return result;
    }

    public async Task<R> AddModal<A, R>(DialogViewModel<A, R> modal, A args)
    {
        _modals.Add(modal);
        var result = await AddDialog(modal, args);
        _modals.Remove(modal);
        return result;
    }
}
