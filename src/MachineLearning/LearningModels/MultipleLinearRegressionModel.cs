using Accord.Statistics.Models.Regression.Linear;

using MachineLearning.Interfaces;

namespace MachineLearning.LearningModels
{
    /// <summary>
    /// Класс модель обучения регрессии с алгоритмом мультилинейной регрессии.
    /// Реализует <see cref="IRegressionModel"/>.
    /// </summary>
    public class MultipleLinearRegressionModel : IRegressionModel
    {
        /// <summary>
        /// Данные для мультилинейной регрессии.
        /// </summary>
        private MultipleLinearRegression? _multipleLinearRegression;

        /// <inheritdoc />
        public Task Train(IEnumerable<IEnumerable<double>> data, IEnumerable<double> targets)
        {
            var ordinaryLeastSquares = new OrdinaryLeastSquares();
            _multipleLinearRegression = ordinaryLeastSquares.Learn
                (data.To2dArray(), targets.ToArray());
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public double Predict(IEnumerable<double> data) =>
            _multipleLinearRegression.Transform(data.ToArray());
    }
}
