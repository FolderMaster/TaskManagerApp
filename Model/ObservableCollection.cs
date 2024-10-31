using System.Collections.Specialized;

namespace Model
{
    public class ObservableCollection : ObservableObject, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? oldItem, object? newItem, int index) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs
                (action, newItem, oldItem, index));

        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? item, int index) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));

        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? item, int index, int oldIndex) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs
                (action, item, index, oldIndex));

        protected void OnCollectionChanged(NotifyCollectionChangedAction action) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action));

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs args) =>
            CollectionChanged?.Invoke(this, args);
    }
}
