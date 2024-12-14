using MachineLearning.LearningEvaluators;

namespace MachineLearning.Tests.LearningEvaluators
{
    public class ClusteringCrossValidationEvaluatorPrototype :
        ClusteringCrossValidationEvaluator
    {
        public IEnumerable<ValidationFold> GetValidationFoldsSet
            (IEnumerable<IEnumerable<double>> data) => GetValidationFolds(data);
    }
}
