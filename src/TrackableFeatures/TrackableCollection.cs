using System.Collections;
using System.Collections.Specialized;

namespace TrackableFeatures
{
    /// <summary>
    /// Базовый класс коллекции, предоставляющий поддержку отслеживания изменений в коллекции.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>. Реализует <see cref="INotifyCollectionChanged"/>,
    /// <see cref="IList"/> и <see cref="IList{T}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип элементов коллекции.</typeparam>
    public class TrackableCollection<T> : TrackableObject,
        IList<T>, IList, INotifyCollectionChanged
    {
        /// <summary>
        /// Список элементов.
        /// </summary>
        protected readonly List<T> _items = new();

        /// <inheritdoc/>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <inheritdoc/>
        public T this[int index]
        {
            get => _items[index];
            set => Replace(index, value);
        }

        /// <inheritdoc/>
        object? IList.this[int index]
        {
            get => _items[index];
            set => Replace(index, (T)value);
        }

        /// <inheritdoc/>
        public int Count => _items.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public bool IsFixedSize => false;

        /// <inheritdoc/>
        public bool IsSynchronized => false;

        /// <inheritdoc/>
        public object SyncRoot => this;

        public TrackableCollection(IEnumerable<T>? items = null)
        {
            if (items?.Any() == true)
            {
                _items = items.ToList();
                foreach (var task in _items)
                {
                    OnAddedItem(task, false);
                }
            }
        }

        /// <inheritdoc/>
        public bool Contains(T item) => _items.Contains(item);

        /// <inheritdoc/>
        public bool Contains(object? value) => Contains((T)value);

        /// <inheritdoc/>
        public int IndexOf(T item) => _items.IndexOf(item);

        /// <inheritdoc/>
        public int IndexOf(object? value) => _items.IndexOf((T)value);

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public void CopyTo(Array array, int index)
        {
            foreach (var item in _items)
            {
                array.SetValue(item, index++);
            }
        }

        /// <inheritdoc/>
        public void Add(T item) => AddItem(item);

        /// <inheritdoc/>
        public int Add(object? value) => AddItem((T)value);

        /// <inheritdoc/>
        public void Insert(int index, T item) => InsertItem(index, item);

        /// <inheritdoc/>
        public void Insert(int index, object? value) => InsertItem(index, (T)value);

        /// <inheritdoc/>
        public bool Remove(T item) => RemoveItem(item);

        /// <inheritdoc/>
        public void Remove(object? value) => RemoveItem((T)value);

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            var item = _items[index];
            _items.RemoveAt(index);
            OnRemovedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
        }

        /// <summary>
        /// Заменяет элемент по указанному индексу на новый.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="item">Элемент.</param>
        public void Replace(int index, T item)
        {
            var oldItem = _items[index];
            _items[index] = item;
            OnRemovedItem(oldItem);
            OnAddedItem(item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, oldItem, item, index);
        }

        /// <summary>
        /// Перемещает элемент из одного индекса в другой.
        /// </summary>
        /// <param name="oldIndex">Старый индекс.</param>
        /// <param name="newIndex">Новый индекс.</param>
        public void Move(int oldIndex, int newIndex)
        {
            var item = _items[oldIndex];
            _items.RemoveAt(oldIndex);
            _items.Insert(newIndex, item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            foreach (var task in _items)
            {
                OnRemovedItem(task);
            }
            _items.Clear();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        /// <summary>
        /// Добавляет элемент в коллекцию.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Индекс добавленного элемента.</returns>
        protected int AddItem(T item)
        {
            _items.Add(item);
            var index = _items.IndexOf(item);
            OnAddedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
            return index;
        }

        /// <summary>
        /// Вставляет элемент в указанную позицию коллекции.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="item">Элемент.</param>
        protected void InsertItem(int index, T item)
        {
            _items.Insert(index, item);
            OnAddedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        /// <summary>
        /// Удаляет элемент из коллекции.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Возвращает <c>true</c>, если элемент был удалён, иначе <c>false</c>.</returns>
        protected bool RemoveItem(T item)
        {
            var index = _items.IndexOf(item);
            var isRemoved = _items.Remove(item);
            if (isRemoved)
            {
                OnRemovedItem(item);
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
            }
            return isRemoved;
        }

        /// <summary>
        /// Вызывается при добавлении элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <param name="arePropertiesUpdate">Флаг обновления свойств.</param>
        protected virtual void OnAddedItem(T item, bool arePropertiesUpdate = true) { }

        /// <summary>
        /// Вызывается при удалении элемента.
        /// </summary>
        /// <param name="item">Удалённый элемент.</param>
        protected virtual void OnRemovedItem(T item) { }

        /// <summary>
        /// Вызывает событие <see cref="CollectionChanged"/> для индекса,
        /// в котором изменился элемент.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="oldItem">Старый элемент.</param>
        /// <param name="newItem">Новый элемент.</param>
        /// <param name="index">Индекс.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? oldItem, object? newItem, int index) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs
                (action, newItem, oldItem, index));

        /// <summary>
        /// Вызывает событие <see cref="CollectionChanged"/> для элемента,
        /// над которым совершили действие.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="item">Элемент.</param>
        /// <param name="index">Индекс.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? item, int index) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));

        /// <summary>
        /// Вызывает событие <see cref="CollectionChanged"/> для элемента, который изменил индекс.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="item">Элемент.</param>
        /// <param name="newIndex">Новый индекс.</param>
        /// <param name="oldIndex">Старый индекс.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? item, int newIndex, int oldIndex) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs
                (action, item, newIndex, oldIndex));

        /// <summary>
        /// Вызывает событие <see cref="CollectionChanged"/> для действия.
        /// </summary>
        /// <param name="action">Действие.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action));

        /// <summary>
        /// Вызывает событие <see cref="CollectionChanged"/> с аргументами.
        /// </summary>
        /// <param name="args">Аргументы.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs args) =>
            CollectionChanged?.Invoke(this, args);
    }
}
