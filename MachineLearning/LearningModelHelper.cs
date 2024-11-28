using MachineLearning.Interfaces.Generals;

namespace MachineLearning
{
    public static class LearningModelHelper
    {
        public static IEnumerable<R> Predict<T, R>(this ILearningModel<T, R> learningModel,
            IEnumerable<IEnumerable<T>> values)
        {
            foreach (var value in values)
            {
                yield return learningModel.Predict(value);
            }
        }
    }
}
