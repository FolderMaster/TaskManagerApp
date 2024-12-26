using MachineLearning;

namespace ViewModel.Interfaces.ModelLearning
{
    public interface ILearningController<D, DT, DR> : IModelTeacher<D>
    {
        public bool IsValidModel { get; }

        public DR Predict(DT data);
    }
}
