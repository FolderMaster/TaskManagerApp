namespace ViewModel.Technicals
{
    public class MetadataFactory : IFactory<object>
    {
        public MetadataFactory() { }

        public object Create() => new Metadata() { Name = "Task" };
    }
}
