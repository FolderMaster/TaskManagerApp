using Model.Interfaces;

namespace ViewModel.Implementations
{
    public interface ISession : IStorageService
    {
        public IList<ITask> Tasks { get; }
    }
}
