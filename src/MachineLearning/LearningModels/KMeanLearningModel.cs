using Accord.MachineLearning;

using MachineLearning.Interfaces;

namespace MachineLearning.LearningModels
{
    public class KMeanLearningModel : IClusteringModel
    {
        private KMeansClusterCollection? _clusters;

        public int NumbersOfClusters { get; set; } = 2;

        public Task Train(IEnumerable<IEnumerable<double>> values)
        {
            var kmeans = new KMeans(NumbersOfClusters);
            _clusters = kmeans.Learn(values.To2dArray());
            return Task.CompletedTask;
        }

        public int Predict(IEnumerable<double> values) => _clusters.Decide(values.ToArray());
    }
}
