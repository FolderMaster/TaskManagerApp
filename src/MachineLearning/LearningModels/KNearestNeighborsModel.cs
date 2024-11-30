using Accord.MachineLearning;

using MachineLearning.Interfaces;

namespace MachineLearning.LearningModels
{
    public class KNearestNeighborsModel : IClassificationModel
    {
        private KNearestNeighbors? _kNearestNeighbors;

        public int NumbersOfNeighbors { get; set; } = 1;

        public Task Train(IEnumerable<IEnumerable<double>> values, IEnumerable<int> targets)
        {
            _kNearestNeighbors = new KNearestNeighbors(NumbersOfNeighbors);
            _kNearestNeighbors.Learn(values.To2dArray(), targets.ToArray());
            return Task.CompletedTask;
        }

        public int Predict(IEnumerable<double> values) =>
            _kNearestNeighbors.Decide(values.ToArray());
    }
}
