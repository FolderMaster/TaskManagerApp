using TrackableFeatures;
using System.Runtime.CompilerServices;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers;

namespace ViewModel.Implementations.DataManagers.Editors
{
    public class TimeIntervalElementsEditorProxy :
        TrackableObject, ITimeIntervalElementsEditorProxy
    {
        public ITimeIntervalElement _target;

        private DateTime _start;

        private DateTime _end;

        public ITimeIntervalElement Target
        {
            get => _target;
            set
            {
                _target = value;
                UpdateProperties();
            }
        }

        public DateTime Start
        {
            get => _start;
            set => UpdateProperty(ref _start, value, () => OnPropertyChanged(nameof(Start)));
        }

        public DateTime End
        {
            get => _end;
            set => UpdateProperty(ref _end, value, () => OnPropertyChanged(nameof(End)));
        }

        public TimeSpan Duration => End - Start;

        public void ApplyChanges()
        {
            Target.Start = Start;
            Target.End = End;
        }

        private void UpdateProperties()
        {
            Start = Target.Start;
            End = Target.End;
        }

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
