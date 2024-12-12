using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Interfaces.DataManagers
{
    public interface ITimeIntervalElementsEditorProxy :
        IEditorProxy<ITimeIntervalElement>, ITimeIntervalElement { }
}
