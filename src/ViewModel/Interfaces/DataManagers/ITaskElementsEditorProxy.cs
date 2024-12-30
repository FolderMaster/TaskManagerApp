using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Interfaces.DataManagers
{
    /// <summary>
    /// Интерфейс заместителя элементарной задачи для редактирования.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IEditorProxy{ITaskElement}"/> и <see cref="ITaskElement"/>.
    /// </remarks>
    public interface ITaskElementsEditorProxy : IEditorProxy<ITaskElement>, ITaskElement { }
}
