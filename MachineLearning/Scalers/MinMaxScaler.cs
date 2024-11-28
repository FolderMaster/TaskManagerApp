using MachineLearning.Interfaces;

namespace MachineLearning.Scalers
{
    public class MinMaxScaler : IScaler
    {
        public double Min { get; private set; }

        public double Max { get; private set; }

        public IEnumerable<double> FitTransform(IEnumerable<double> data)
        {
            var array = data.ToArray();

            Min = array.Min();
            Max = array.Max();
            var result = new double[data.Count()];
            for (var i = 0; i < data.Count(); ++i)
            {
                result[i] = Transform(array[i]);
            }
            return result;
        }

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
