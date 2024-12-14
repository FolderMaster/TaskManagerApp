using MachineLearning.LearningEvaluators;

namespace MachineLearning.Tests.LearningEvaluators
{
    public class ClassificationCrossValidationEvaluatorPrototype :
        ClassificationCrossValidationEvaluator
    {
        public IEnumerable<ValidationFold> GetValidationFoldsSet
            (IEnumerable<IEnumerable<double>> data, IEnumerable<int> targets) =>
            GetValidationFolds(data, targets);
    }
}
