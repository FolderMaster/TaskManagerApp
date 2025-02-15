using MachineLearning.Interfaces;

namespace MachineLearning.Scalers
{
    /// <summary>
    /// Класс масштабрирования данных с помощью минимума и максимума.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IScaler"/>.
    /// </remarks>
    public class MinMaxScaler : IScaler
    {
        /// <summary>
        /// Возвращает минимальное значение.
        /// </summary>
        public double Min { get; private set; }

        /// <summary>
        /// Возвращает максимальное значение.
        /// </summary>
        public double Max { get; private set; }

        /// <inheritdoc />
        public IEnumerable<double> FitTransform(IEnumerable<double> data)
        {
            var array = data.ToArray();
            var count = data.Count();

            Min = array.Min();
            Max = array.Max();
            
            var result = new double[count];
            for (var i = 0; i < count; ++i)
            {
                result[i] = Transform(array[i]);
            }
            return result;
        }

        /// <inheritdoc />
        public double Transform(double value)
        {
            if (value <= Min)
            {
                return 0;
            }
            if (value >= Max)
            {
                return 1;
            }
            return (value - Min) / (Max - Min);
        }
    }
}
