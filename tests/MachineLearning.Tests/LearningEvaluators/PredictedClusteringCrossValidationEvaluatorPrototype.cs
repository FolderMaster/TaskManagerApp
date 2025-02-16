using MachineLearning.LearningEvaluators;

namespace MachineLearning.Tests.LearningEvaluators
{
    public class PredictedClusteringCrossValidationEvaluatorPrototype :
        PredictedClusteringCrossValidationEvaluator
    {
        public IEnumerable<ValidationFold> GetValidationFoldsSet
            (IEnumerable<IEnumerable<double>> data) => GetValidationFolds(data);

        public IEnumerable<IEnumerable<int>> GetSecondaryTrainIndicesSet
            (IEnumerable<int> trainIndices) => GetSecondaryTrainIndices(trainIndices);
    }
}
