namespace ViewModel.Interfaces.ModelLearning
{
    public interface IModelTeacher<D>
    {
        public Task<bool> Train(IEnumerable<D> data);
    }
}
