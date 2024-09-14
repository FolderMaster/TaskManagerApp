using ReactiveUI;
using System.Runtime.CompilerServices;

namespace ViewModel.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected bool UpdateProperty<T>(ref T field, T? newValue,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T?>.Default.Equals(field, newValue))
        {
            return false;
        }
        this.RaisePropertyChanging(propertyName);
        field = newValue;
        this.RaisePropertyChanged(propertyName);
        return true;
    }
}
