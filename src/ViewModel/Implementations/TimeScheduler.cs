using DynamicData;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Timers;

using ViewModel.Interfaces;

using Timer = System.Timers.Timer;

namespace ViewModel.Implementations
{
    public class TimeScheduler : ITimeScheduler
    {
        private readonly Timer _timer = new();

        private readonly ObservableCollection<DateTime> _timepoints = new();

        private readonly object _lock = new();

        private bool _isUpdateScheduler = true;

        private DateTime _selectedTimepoint;

        public IList<DateTime> Timepoints => _timepoints;

        public event EventHandler<DateTime> TimepointReached;

        public TimeScheduler()
        {
            _timepoints.CollectionChanged += Timepoints_CollectionChanged;
            _timer.Elapsed += Timer_Elapsed;
        }

        private void RescheduleTimer()
        {
            if (!_timepoints.Any())
            {
                _timer.Stop();
                return;
            }

            var nextTimepoints = _timepoints.Where(tp => tp > DateTime.Now).
                OrderBy(tp => tp).ToList();
            _selectedTimepoint = nextTimepoints.FirstOrDefault();
            if (_selectedTimepoint == default)
            {
                _timer.Stop();

                _isUpdateScheduler = false;
                _timepoints.Clear();
                _isUpdateScheduler = true;
            }
            else
            {
                _timer.Stop();
                var interval = (_selectedTimepoint - DateTime.Now).TotalMilliseconds;
                _timer.Interval = interval;
                _timer.Start();

                _isUpdateScheduler = false;
                _timepoints.Clear();
                _timepoints.AddRange(nextTimepoints);
                _isUpdateScheduler = true;
            }
        }

        private void Timepoints_CollectionChanged(object? sender,
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var newItem = (DateTime)e.NewItems[0];
                    if (newItem <= DateTime.Now)
                    {
                        _timepoints.Remove(newItem);
                    }
                    else if (_isUpdateScheduler)
                    {
                        RescheduleTimer();
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    if (_isUpdateScheduler)
                    {
                        RescheduleTimer();
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RescheduleTimer();
                    break;
            }

        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            TimepointReached?.Invoke(this, _selectedTimepoint);
            RescheduleTimer();
        }
    }
}
