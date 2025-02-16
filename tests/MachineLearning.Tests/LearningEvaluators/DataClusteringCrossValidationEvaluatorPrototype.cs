using MachineLearning.LearningEvaluators;

namespace MachineLearning.Tests.LearningEvaluators
{
    public class DataClusteringCrossValidationEvaluatorPrototype :
        DataClusteringCrossValidationEvaluator
    {
        public IEnumerable<ValidationFold> GetValidationFoldsSet
            (IEnumerable<IEnumerable<double>> data) => GetValidationFolds(data);
    }
}
