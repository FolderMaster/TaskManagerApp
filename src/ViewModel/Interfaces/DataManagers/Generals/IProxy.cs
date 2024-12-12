namespace ViewModel.Interfaces.DataManagers.Generals
{
    public interface IProxy<out T>
    {
        public T Target { get; }
    }
}
