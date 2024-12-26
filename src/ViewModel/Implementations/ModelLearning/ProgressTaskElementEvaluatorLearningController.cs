using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Implementations.ModelLearning.Converters;

namespace ViewModel.Implementations.ModelLearning
{
    public class ProgressTaskElementEvaluatorLearningController :
        BaseSupervisedEvaluatorLearningController
        <IEnumerable<double>, double, ITaskElement, ITaskElement, double>
    {
        public ProgressTaskElementEvaluatorLearningController
            (ProgressTaskElementLearningConverter converter,
            IRegressionModel model, IRegressionEvaluator evaluator) :
            base(converter, model, evaluator) { }
    }
}
