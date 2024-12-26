using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Implementations.ModelLearning.Converters;

namespace ViewModel.Implementations.ModelLearning
{
    public class DeadlineTaskElementEvaluatorLearningController :
        BaseSupervisedEvaluatorLearningController
        <IEnumerable<double>, double, ITaskElement, ITaskElement, DateTime?>
    {
        public DeadlineTaskElementEvaluatorLearningController
            (DeadlineTaskElementLearningConverter converter,
            IRegressionModel model, IRegressionEvaluator evaluator) :
            base(converter, model, evaluator) { }
    }
}
