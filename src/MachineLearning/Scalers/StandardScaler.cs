using MachineLearning.Interfaces;

namespace MachineLearning.Scalers
{
    /// <summary>
    /// Класс масштабрирования данных с помощью среднего значения и
    /// среднеквадратического отклонения.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IScaler"/>.
    /// </remarks>
    public class StandardScaler : IScaler
    {
        /// <summary>
        /// Возвращает среднее значение.
        /// </summary>
        public double Mean { get; private set; }

        /// <summary>
        /// Возвращает среднеквадратическое отклонение.
        /// </summary>
        public double StandardDeviation { get; private set; }

        /// <inheritdoc />
        public IEnumerable<double> FitTransform(IEnumerable<double> data)
        {
            Mean = data.Average();
            StandardDeviation = Math.Sqrt(data.Average(v => Math.Pow(v - Mean, 2)));
            return data.Select(Transform);
        }

        /// <inheritdoc />
        public double Transform(double value)
        {
            return (value - Mean) / StandardDeviation;
        }
    }
}
