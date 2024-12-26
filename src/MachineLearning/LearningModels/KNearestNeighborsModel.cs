using Accord.MachineLearning;

using TrackableFeatures;

using MachineLearning.Interfaces;
using MachineLearning.DistanceMetrics;

namespace MachineLearning.LearningModels
{
    /// <summary>
    /// Класс модель обучения классификации с алгоритмом K-ближащих соседей.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>. Реализует <see cref="IClassificationModel"/>.
    /// </remarks>
    public class KNearestNeighborsModel : TrackableObject, IClassificationModel
    {
        /// <summary>
        /// Адаптер метрики расстояний.
        /// </summary>
        private readonly MetricAdapter _metricAdapter = new();

        /// <summary>
        /// Данные для алгоритма K-ближайших соседей.
        /// </summary>
        private KNearestNeighbors? _kNearestNeighbors;

        /// <summary>
        /// Количество соседей.
        /// </summary>
        private int _numberOfNeighbors = 1;

        /// <summary>
        /// Метрика расстояний.
        /// </summary>
        private IPointDistanceMetric _distanceMetric = new EuclideanDistanceMetric();

        /// <summary>
        /// Возвращает и задаёт количество соседей.
        /// </summary>
        public int NumberOfNeighbors
        {
            get => _numberOfNeighbors;
            set => UpdateProperty(ref _numberOfNeighbors, value, OnPropertyChanged);
        }

        /// <summary>
        /// Возвращает и задаёт метрику расстояний.
        /// </summary>
        public IPointDistanceMetric DistanceMetric
        {
            get => _distanceMetric;
            set => UpdateProperty(ref _distanceMetric, value, OnPropertyChanged);
        }

        /// <inheritdoc />
        public Task Train(IEnumerable<IEnumerable<double>> data, IEnumerable<int> targets)
        {
            _metricAdapter.DistanceMetric = DistanceMetric;
            _kNearestNeighbors = new KNearestNeighbors(NumberOfNeighbors, _metricAdapter);
            _kNearestNeighbors.Learn(data.To2dArray(), targets.ToArray());
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public int Predict(IEnumerable<double> data) =>
            _kNearestNeighbors.Decide(data.ToArray());

        /// <summary>
        /// Вызывается при изменении свойства.
        /// </summary>
        private void OnPropertyChanged()
        {
            ClearAllErrors();
            if (NumberOfNeighbors <= 0)
            {
                AddError($"{nameof(NumberOfNeighbors)} должно быть больше 0!");
            }
            if (DistanceMetric == null)
            {
                AddError($"{nameof(DistanceMetric)} должно быть назначено!");
            }
        }
    }
}
