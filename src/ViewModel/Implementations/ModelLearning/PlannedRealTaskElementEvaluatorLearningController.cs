using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Implementations.ModelLearning.Converters;

namespace ViewModel.Implementations.ModelLearning
{
    public class PlannedRealTaskElementEvaluatorLearningController :
        BaseSupervisedEvaluatorLearningController
        <IEnumerable<double>, double, ITaskElement, ITaskElement, double>
    {
        public PlannedRealTaskElementEvaluatorLearningController
            (PlannedRealTaskElementLearningConverter converter,
            IRegressionModel model, IRegressionEvaluator evaluator) :
            base(converter, model, evaluator) { }
    }
}
