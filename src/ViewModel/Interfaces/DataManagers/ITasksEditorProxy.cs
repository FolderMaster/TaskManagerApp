using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Interfaces.DataManagers
{
    /// <summary>
    /// Интерфейс заместителя задачи для редактирования.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IEditorProxy{ITask}"/> и <see cref="ITask"/>.
    /// </remarks>
    public interface ITasksEditorProxy : IEditorProxy<ITask>, ITask { }
}
