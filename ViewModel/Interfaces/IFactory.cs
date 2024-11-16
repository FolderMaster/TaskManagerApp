namespace ViewModel.Interfaces
{
    public interface IFactory<T>
    {
        public T Create();
    }
}
