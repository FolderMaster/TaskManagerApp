namespace ViewModel.Implementations.Editors
{
    public class PrototypeProperty<T>
    {
        private T _newValue;

        public bool IsChanged { get; private set; } = false;

        public T NewValue
        {
            get => _newValue;
            set
            {
                _newValue = value;
                IsChanged = OldValue != null && !OldValue.Equals(value) ||
                    value != null && !value.Equals(OldValue);
            }
        }

        public T OldValue { get; private set; }

        public PrototypeProperty(T oldValue)
        {
            OldValue = oldValue;
            _newValue = oldValue;
        }
    }
}
