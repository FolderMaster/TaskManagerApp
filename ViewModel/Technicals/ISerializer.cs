namespace ViewModel.Technicals
{
    public interface ISerializer
    {
        byte[] Serialize(object value);

        T? Deserialize<T>(byte[] data);
    }
}
