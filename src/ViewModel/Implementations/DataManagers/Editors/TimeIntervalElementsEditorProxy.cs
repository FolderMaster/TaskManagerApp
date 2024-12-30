using System.Runtime.CompilerServices;

using TrackableFeatures;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers;

namespace ViewModel.Implementations.DataManagers.Editors
{
    /// <summary>
    /// Класс заместитель элементарного временного интервала для редактирования.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="ITimeIntervalElementsEditorProxy"/>.
    /// </remarks>
    public class TimeIntervalElementsEditorProxy :
        TrackableObject, ITimeIntervalElementsEditorProxy
    {
        /// <summary>
        /// Заменяемый объект.
        /// </summary>
        public ITimeIntervalElement _target;

        /// <summary>
        /// Начало.
        /// </summary>
        private DateTime _start;

        /// <summary>
        /// Конец.
        /// </summary>
        private DateTime _end;

        /// <inheritdoc/>
        public ITimeIntervalElement Target
        {
            get => _target;
            set
            {
                _target = value;
                UpdateProperties();
            }
        }

        /// <inheritdoc/>
        public DateTime Start
        {
            get => _start;
            set => UpdateProperty(ref _start, value, (s, e) => OnPropertyChanged(nameof(Start)));
        }

        /// <inheritdoc/>
        public DateTime End
        {
            get => _end;
            set => UpdateProperty(ref _end, value, (s, e) => OnPropertyChanged(nameof(End)));
        }

        /// <inheritdoc/>
        public TimeSpan Duration => End - Start;

        /// <inheritdoc/>
        public void ApplyChanges()
        {
            Target.Start = Start;
            Target.End = End;
        }

        /// <summary>
        /// Обновляет свойства.
        /// </summary>
        private void UpdateProperties()
        {
            Start = Target.Start;
            End = Target.End;
        }

        /// <summary>
        /// Вызываетcz при событии изменения свойства.
        /// </summary>
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ClearAllErrors();
            if (Start > End)
            {
                if (propertyName == nameof(Start))
                {
                    AddError($"{nameof(Start)} находится после {nameof(End)}", nameof(Start));
                }
                else if (propertyName == nameof(End))
                {
                    AddError($"{nameof(End)} находится до {nameof(Start)}", nameof(End));
                }
            }
            base.OnPropertyChanged(nameof(Duration));
        }
    }
}
