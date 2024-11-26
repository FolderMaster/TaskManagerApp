namespace MachineLearning.Interfaces
{
    public interface ILearningModel<T, R>
    {
        public R Predict(IEnumerable<T> values);
    }
}
