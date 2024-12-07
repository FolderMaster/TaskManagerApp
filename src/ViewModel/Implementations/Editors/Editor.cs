﻿using ViewModel.Interfaces;
using ViewModel.Technicals;
using Model.Interfaces;

namespace ViewModel.Implementations.Editors
{
    internal class Editor : IEditor<ITask>
    {
        private TaskEditorPrototype? _prototype;

        public object? Prototype => _prototype;

        public void ApplyEditing(IEnumerable<ITask> editables)
        {
            if (editables?.Any() != false)
            {
                throw new ArgumentException(nameof(editables));
            }
            var isTaskElements = editables.
                All(t => t.GetType().IsAssignableTo(typeof(ITaskElement)));
            var prototype = isTaskElements ?
                new TaskElementEditorPrototype() : new TaskEditorPrototype();
            prototype.Metadata = new Metadata()
            {
                Title = null,
                Tags = null
            };
            if (editables.All(t => ((Metadata)t.Metadata).Title ==
                ((Metadata)editables.First().Metadata).Title))
            {
                ((Metadata)prototype.Metadata).Title = ((Metadata)editables.First().Metadata).Title;
            }
            if (editables.All(t => ((Metadata)t.Metadata).Tags.OrderBy(a => a).
                SequenceEqual(((Metadata)editables.First().Metadata).Tags.OrderBy(a => a))))
            {
                ((Metadata)prototype.Metadata).Tags = ((Metadata)editables.First().Metadata).Tags;
            }
            if (isTaskElements)
            {
                var taskElementPrototype = prototype as TaskElementEditorPrototype;
                var taskElements = editables.Cast<ITaskElement>();
            }
            _prototype = prototype;
        }

        public void CreatePrototype(IEnumerable<ITask> editables)
        {
            if (editables?.Any() != false)
            {
                throw new ArgumentException(nameof(editables));
            }
        }

        private void Foreach<E>(IEnumerable<E> values, Action<E> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }
    }
}
