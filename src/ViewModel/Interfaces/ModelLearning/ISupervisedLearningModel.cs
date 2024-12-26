using MachineLearning.Interfaces.Generals;

namespace ViewModel.Interfaces.ModelLearning
{
    public interface ISupervisedLearningController<T, R, D, DT, DR> : ILearningController<D, DT, DR>
    {
        public ISupervisedLearningModel<T, R> LearningModel { get; set; }

        public ISupervisedLearningEvaluator<T, R> LearningEvaluator { get; set; }
    }
}
