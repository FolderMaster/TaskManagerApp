using MachineLearning.LearningEvaluators;

namespace MachineLearning.Tests.LearningEvaluators
{
    public class RegressionCrossValidationEvaluatorPrototype :
        RegressionCrossValidationEvaluator
    {
        public IEnumerable<ValidationFold> GetValidationFoldsSet
            (IEnumerable<IEnumerable<double>> data, IEnumerable<double> targets) =>
            GetValidationFolds(data, targets);
    }
}
