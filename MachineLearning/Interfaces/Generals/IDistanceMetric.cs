namespace MachineLearning.Interfaces.Generals
{
    public interface IDistanceMetric<T>
    {
        public double CalculateDistance(T value1, T value2);
    }
}
