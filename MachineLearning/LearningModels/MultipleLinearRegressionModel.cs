using Accord.Statistics.Models.Regression.Linear;

using MachineLearning.Interfaces;

namespace MachineLearning.LearningModels
{
    public class MultipleLinearRegressionModel : IRegressionModel
    {
        private MultipleLinearRegression? _multipleLinearRegression;

        public Task Train(IEnumerable<IEnumerable<double>> values, IEnumerable<double> targets)
        {
            var ordinaryLeastSquares = new OrdinaryLeastSquares();
            _multipleLinearRegression = ordinaryLeastSquares.Learn
                (values.To2dArray(), targets.ToArray());
            return Task.CompletedTask;
        }

        public double Predict(IEnumerable<double> values) =>
            _multipleLinearRegression.Transform(values.ToArray());
    }
}
