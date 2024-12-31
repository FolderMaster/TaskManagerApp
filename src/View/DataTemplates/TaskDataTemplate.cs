using Avalonia.Controls.Templates;
using Avalonia.Controls;
using Avalonia.Data;

using Model.Interfaces;

namespace View.DataTemplates
{
    /// <summary>
    /// Класс шаблона данных для задач.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ITreeDataTemplate"/>.
    /// </remarks>
    public class TaskDataTemplate : ITreeDataTemplate
    {
        /// <summary>
        /// Возвращает и задаёт шаблона данных для элементарной задачи.
        /// </summary>
        public IDataTemplate? ElementDataTemplate { get; set; }

        /// <summary>
        /// Возвращает и задаёт шаблона данных для составной задачи.
        /// </summary>
        public IDataTemplate? CompositeDataTemplate { get; set; }

        /// <summary>
        /// Возвращает и задаёт шаблона данных для задачи.
        /// </summary>
        public IDataTemplate? DataTemplate { get; set; }

        /// <inheritdoc/>
        public Control? Build(object? param)
        {
            if (param is ITaskElement && ElementDataTemplate != null)
            {
                return ElementDataTemplate.Build(param);
            }
            else if (param is ITaskComposite && CompositeDataTemplate != null)
            {
                return CompositeDataTemplate.Build(param);
            }
            else if (DataTemplate != null)
            {
                return DataTemplate.Build(param);
            }
            return null;
        }

        /// <inheritdoc/>
        public InstancedBinding? ItemsSelector(object item)
        {
            if (item is ITaskElement && ElementDataTemplate != null &&
                ElementDataTemplate is ITreeDataTemplate treeTemplate1)
            {
                return treeTemplate1.ItemsSelector(item);
            }
            else if (item is ITaskComposite && CompositeDataTemplate != null &&
                CompositeDataTemplate is ITreeDataTemplate treeTemplate2)
            {
                return treeTemplate2.ItemsSelector(item);
            }
            else if (DataTemplate != null &&
                ElementDataTemplate is ITreeDataTemplate treeTemplate3)
            {
                return treeTemplate3.ItemsSelector(item);
            }
            return null;
        }

        /// <inheritdoc/>
        public bool Match(object? data) => data is ITask;
    }
}
