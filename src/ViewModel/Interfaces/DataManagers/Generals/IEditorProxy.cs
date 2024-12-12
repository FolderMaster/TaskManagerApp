namespace ViewModel.Interfaces.DataManagers.Generals
{
    public interface IEditorProxy<T> : IProxy<T>, IEditorService
    {
        public new T Target { get; set; }
    }
}
