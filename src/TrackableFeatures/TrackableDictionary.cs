using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace TrackableFeatures
{
    /// <summary>
    /// Базовый класс словаря, предоставляющий поддержку отслеживания изменений в словаре.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="INotifyCollectionChanged"/>, <see cref="IDictionary"/>,
    /// <see cref="IDictionary{K, V}"/> и <see cref="IReadOnlyDictionary{K, V}"/>.
    /// </remarks>
    /// <typeparam name="K">Тип ключей коллекции.</typeparam>
    /// <typeparam name="V">Тип значений коллекции.</typeparam>
    public class TrackableDictionary<K, V> : TrackableObject,
        IDictionary, IDictionary<K, V>, IReadOnlyDictionary<K, V>, INotifyCollectionChanged
    {
        /// <summary>
        /// Словарь.
        /// </summary>
        private readonly Dictionary<K, V> _dictionary = new();

        /// <inheritdoc/>
        public object? this[object key]
        {
            get => this[(K)key];
            set => this[(K)key] = (V)value;
        }

        /// <inheritdoc/>
        public V this[K key]
        {
            get => _dictionary[key];
            set
            {
                if (_dictionary.ContainsKey(key))
                {
                    var oldValue = _dictionary[key];
                    _dictionary[key] = value;
                    var oldKeyValuePair = new KeyValuePair<K, V>(key, oldValue);
                    var newKeyValuePair = new KeyValuePair<K, V>(key, value);
                    OnRemovedItem(oldKeyValuePair);
                    OnAddedItem(newKeyValuePair);
                    OnPropertyChanged("Item[]");
                    OnCollectionChanged(NotifyCollectionChangedAction.Replace,
                        oldKeyValuePair, newKeyValuePair);
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        /// <inheritdoc/>
        public bool IsFixedSize => false;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public ICollection Keys => _dictionary.Keys;

        /// <inheritdoc/>
        public ICollection Values => _dictionary.Values;

        /// <inheritdoc/>
        ICollection<K> IDictionary<K, V>.Keys => _dictionary.Keys;

        /// <inheritdoc/>
        ICollection<V> IDictionary<K, V>.Values => _dictionary.Values;

        /// <inheritdoc/>
        IEnumerable<K> IReadOnlyDictionary<K, V>.Keys => _dictionary.Keys;

        /// <inheritdoc/>
        IEnumerable<V> IReadOnlyDictionary<K, V>.Values => _dictionary.Values;

        /// <inheritdoc/>
        public int Count => _dictionary.Count;

        /// <inheritdoc/>
        public bool IsSynchronized => false;

        /// <inheritdoc/>
        public object SyncRoot => this;

        /// <inheritdoc/>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TrackableDictionary{K, V}"/>.
        /// </summary>
        /// <param name="dictionary">Словарь.</param>
        public TrackableDictionary(IDictionary<K, V>? dictionary = null)
        {
            if (dictionary?.Any() == true)
            {
                _dictionary = dictionary.ToDictionary();
                foreach (var item in _dictionary)
                {
                    OnAddedItem(item, false);
                }
            }
        }

        /// <inheritdoc/>
        public void Add(object key, object? value) => Add((K)key, (V)value);

        /// <inheritdoc/>
        public void Add(KeyValuePair<K, V> item) => Add(item.Key, item.Value);

        /// <inheritdoc/>
        public void Add(K key, V value)
        {
            _dictionary.Add(key, value);
            var keyValuePair = new KeyValuePair<K, V>(key, value);
            OnAddedItem(keyValuePair);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, keyValuePair);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            foreach (var item in _dictionary)
            {
                OnRemovedItem(item);
            }
            _dictionary.Clear();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        /// <inheritdoc/>
        public bool Contains(object key) => _dictionary.ContainsKey((K)key);

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<K, V> item) => _dictionary.Contains(item);

        /// <inheritdoc/>
        public bool ContainsKey(K key) => _dictionary.ContainsKey(key);

        /// <inheritdoc/>
        public void CopyTo(Array array, int index)
        {
            foreach (var item in _dictionary)
            {
                array.SetValue(item, index++);
            }
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) =>
            CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public void Remove(object key) => Remove((K)key);

        /// <inheritdoc/>
        public bool Remove(K key)
        {
            var isRemoved = _dictionary.TryGetValue(key, out V value) && _dictionary.Remove(key);
            if (isRemoved)
            {
                var keyValuePair = new KeyValuePair<K, V>(key, value);
                OnRemovedItem(keyValuePair);
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, keyValuePair);
            }
            return isRemoved;
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<K, V> item)
        {
            var isRemoved = !_dictionary.TryGetValue(item.Key, out V value) &&
                EqualityComparer<V>.Default.Equals(value, item.Value) &&
                _dictionary.Remove(item.Key);
            if (isRemoved)
            {
                OnRemovedItem(item);
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
            }
            return isRemoved;
        }

        /// <inheritdoc/>
        public bool TryGetValue(K key, [MaybeNullWhen(false)] out V value) =>
            _dictionary.TryGetValue(key, out value);

        /// <inheritdoc/>
        public IDictionaryEnumerator GetEnumerator() => _dictionary.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator() =>
            _dictionary.GetEnumerator();

        /// <summary>
        /// Вызывается при добавлении элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <param name="arePropertiesUpdate">Флаг обновления свойств.</param>
        protected internal virtual void OnAddedItem(KeyValuePair<K, V> item,
            bool arePropertiesUpdate = true) { }

        /// <summary>
        /// Вызывается при удалении элемента.
        /// </summary>
        /// <param name="item">Удалённый элемент.</param>
        protected internal virtual void OnRemovedItem(KeyValuePair<K, V> item) { }

        /// <summary>
        /// Вызывает событие <see cref="CollectionChanged"/> для индекса,
        /// в котором изменился элемент.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="oldItem">Старый элемент.</param>
        /// <param name="newItem">Новый элемент.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? oldItem, object? newItem) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs
                (action, newItem, oldItem));

        /// <summary>
        /// Вызывает событие <see cref="CollectionChanged"/> для элемента,
        /// над которым совершили действие.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="item">Элемент.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? item) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item));

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
