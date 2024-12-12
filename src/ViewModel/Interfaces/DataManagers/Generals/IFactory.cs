namespace ViewModel.Interfaces.DataManagers.Generals
{
    public interface IFactory<out T>
    {
        public T Create();
    }
}
