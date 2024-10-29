namespace ViewModel.Technicals
{
    public interface IFactory<T>
    {
        public T Create();
    }
}
