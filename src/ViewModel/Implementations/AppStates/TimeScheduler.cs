using DynamicData;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Timers;

using ViewModel.Interfaces;

using Timer = System.Timers.Timer;

namespace ViewModel.Implementations.AppStates
{
    /// <summary>
    /// Класс планировщика времени, который отслеживает достижение временных меток.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="ITimeScheduler"/>.
    /// </remarks>
    public class TimeScheduler : ITimeScheduler
    {
        /// <summary>
        /// Таймер.
        /// </summary>
        private readonly Timer _timer = new();

        /// <summary>
        /// Временные метки.
        /// </summary>
        private readonly ObservableCollection<DateTime> _timepoints = new();

        /// <summary>
        /// Логическое значение, указывающее на обновление планировщика.
        /// </summary>
        private bool _isUpdateScheduler = true;

        /// <summary>
        /// Выбранная  временная метка.
        /// </summary>
        private DateTime _selectedTimepoint;

        /// <inheritdoc/>
        public IList<DateTime> Timepoints => _timepoints;

        /// <inheritdoc/>
        public event EventHandler<DateTime> TimepointReached;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeScheduler"/> по умолчанию.
        /// </summary>
        public TimeScheduler()
        {
            _timepoints.CollectionChanged += Timepoints_CollectionChanged;
            _timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// Перестраивает таймера планировщика.
        /// </summary>
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
