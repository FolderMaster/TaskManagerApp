using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Implementations.ModelLearning.Converters;

namespace ViewModel.Implementations.ModelLearning
{
    public class ExecutionChanceTaskElementEvaluatorLearningController :
        BaseSupervisedEvaluatorLearningController
        <IEnumerable<double>, double, ITaskElement, ITaskElement, double>
    {
        public ExecutionChanceTaskElementEvaluatorLearningController
            (ExecutionChanceTaskElementLearningConverter converter,
            IRegressionModel model, IRegressionEvaluator evaluator) :
            base(converter, model, evaluator) { }
    }
}
