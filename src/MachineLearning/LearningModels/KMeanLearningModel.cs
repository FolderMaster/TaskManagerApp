using Accord.MachineLearning;

using TrackableFeatures;

using MachineLearning.Interfaces;
using MachineLearning.DistanceMetrics;

namespace MachineLearning.LearningModels
{
    /// <summary>
    /// Класс модель обучения кластеризации с алгоритмом K-средних.
    /// Наследует <see cref="TrackableObject"/>. Реализует <see cref="IClusteringModel"/>.
    /// </summary>
    public class KMeanLearningModel : TrackableObject, IClusteringModel
    {
        /// <summary>
        /// Адаптер метрики расстояний.
        /// </summary>
        private readonly MetricAdapter _metricAdapter = new();

        /// <summary>
        /// Данные коллекции кластеров.
        /// </summary>
        private KMeansClusterCollection? _clusters;

        /// <summary>
        /// Количество кластеров.
        /// </summary>
        private int _numbersOfClusters = 2;

        /// <summary>
        /// Метрика расстояний.
        /// </summary>
        private IPointDistanceMetric _distanceMetric = new EuclideanDistanceMetric();

        /// <summary>
        /// Возвращает и задаёт количество кластеров.
        /// </summary>
        public int NumbersOfClusters
        {
            get => _numbersOfClusters;
            set => UpdateProperty(ref _numbersOfClusters, value, OnPropertyChanged);
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
        public Task Train(IEnumerable<IEnumerable<double>> data)
        {
            _metricAdapter.DistanceMetric = DistanceMetric;
            var kmeans = new KMeans(NumbersOfClusters, _metricAdapter);
            _clusters = kmeans.Learn(data.To2dArray());
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public int Predict(IEnumerable<double> data) => _clusters.Decide(data.ToArray());

        /// <summary>
        /// Вызывается при изменении свойства.
        /// </summary>
        private void OnPropertyChanged()
        {
            ClearErrors(nameof(NumbersOfClusters));
            if (NumbersOfClusters <= 1)
            {
                AddError($"{nameof(NumbersOfClusters)} должно быть больше 1!");
            }
            if (DistanceMetric == null)
            {
                AddError($"{nameof(DistanceMetric)} должно быть назначено!");
            }
        }
    }
}
