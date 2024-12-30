using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Interfaces.DataManagers
{
    /// <summary>
    /// Интерфейс заместителя элементарного временного интервала для редактирования.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IEditorProxy{ITimeIntervalElement}"/> и
    /// <see cref="ITimeIntervalElement"/>.
    /// </remarks>
    public interface ITimeIntervalElementsEditorProxy :
        IEditorProxy<ITimeIntervalElement>, ITimeIntervalElement { }
}
