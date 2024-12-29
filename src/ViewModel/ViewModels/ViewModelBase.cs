using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;

namespace ViewModel.ViewModels;

public partial class ViewModelBase : ReactiveObject, IActivatableViewModel
{
    protected readonly IObservable<bool> _modalsObservable;

    private ObservableCollection<ViewModelBase> _dialogs = new();

    private ObservableCollection<ViewModelBase> _modals = new();

    public ObservableCollection<ViewModelBase> Dialogs => _dialogs;

    public ObservableCollection<ViewModelBase> Modals => _modals;

    public ViewModelActivator Activator { get; private set; } = new ViewModelActivator();

    public ViewModelBase()
    {
        _modalsObservable = this.WhenAnyValue(x => x._modals).Select(x => x.Count == 0);
    }

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
