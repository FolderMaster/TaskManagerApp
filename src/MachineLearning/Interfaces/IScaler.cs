namespace MachineLearning.Interfaces
{
    public interface IScaler
    {
        public IEnumerable<double> FitTransform(IEnumerable<double> data);

        public double Transform(double value);
    }
}
