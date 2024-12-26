using MachineLearning.Interfaces.Generals;

namespace ViewModel.Interfaces.ModelLearning
{
    public interface IUnsupervisedLearningController<T, R, D, DT, DR> : ILearningController<D, DT, DR>
    {
        public IUnsupervisedLearningModel<T, R> LearningModel { get; set; }

        public IUnsupervisedLearningEvaluator<T, R> LearningEvaluator { get; set; }
    }
}
