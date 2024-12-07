namespace ViewModel.Implementations.Sessions.Database.Mappers
{
    public interface IMapper<T1, T2>
    {
        public T2 Map(T1 value);

        public T1 MapBack(T2 value);
    }
}
