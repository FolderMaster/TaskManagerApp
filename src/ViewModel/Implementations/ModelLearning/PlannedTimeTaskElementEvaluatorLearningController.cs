using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Implementations.ModelLearning.Converters;

namespace ViewModel.Implementations.ModelLearning
{
    public class PlannedTimeTaskElementEvaluatorLearningController :
        BaseSupervisedEvaluatorLearningController
        <IEnumerable<double>, double, ITaskElement, ITaskElement, TimeSpan>
    {
        public PlannedTimeTaskElementEvaluatorLearningController
            (PlannedTimeTaskElementLearningConverter converter,
            IRegressionModel model, IRegressionEvaluator evaluator) :
            base(converter, model, evaluator) { }
    }
}
