namespace TrackableFeatures.Tests
{
    public class FakeTrackableCollection : TrackableCollection<object>
    {
        private object? _lastEditedItem;

        public object? LastEditedItem
        {
            get => _lastEditedItem;
            set => UpdateProperty(ref _lastEditedItem, value);
        }

        public FakeTrackableCollection(IEnumerable<object>? items = null) : base(items) { }

        protected internal override void OnAddedItem(object item, bool arePropertiesUpdate = true)
        {
            if (arePropertiesUpdate)
            {
                LastEditedItem = item;
            }
        }

        protected internal override void OnRemovedItem(object item)
        {
            LastEditedItem = item;
        }
    }
}
