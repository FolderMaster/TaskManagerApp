namespace MachineLearning.Interfaces.Generals
{
    public interface ILearningModel<T, R>
    {
        public R Predict(IEnumerable<T> values);
    }
}
