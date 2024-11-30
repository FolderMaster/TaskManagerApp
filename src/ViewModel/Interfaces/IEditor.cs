namespace ViewModel.Interfaces
{
    public interface IEditor<T>
    {
        public object? Prototype { get; }

        public void CreatePrototype(IEnumerable<T> editables);

        public void ApplyEditing(IEnumerable<T> editables);
    }
}
