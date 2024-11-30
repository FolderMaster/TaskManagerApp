namespace ViewModel.Interfaces
{
    public interface ISerializer
    {
        byte[] Serialize(object value);

        T? Deserialize<T>(byte[] data);
    }
}
