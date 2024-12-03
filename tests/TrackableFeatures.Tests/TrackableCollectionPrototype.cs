namespace TrackableFeatures.Tests
{
    public class TrackableCollectionPrototype : TrackableCollection<object>
    {
        private object? _lastEditedItem;

        public object? LastEditedItem
        {
            get => _lastEditedItem;
            set => UpdateProperty(ref _lastEditedItem, value);
        }

        public TrackableCollectionPrototype(IEnumerable<object>? items = null) : base(items) { }

        protected override void OnAddedItem(object item, bool arePropertiesUpdate = true)
        {
            if (arePropertiesUpdate)
            {
                LastEditedItem = item;
            }
        }

        protected override void OnRemovedItem(object item)
        {
            LastEditedItem = item;
        }
    }
}
