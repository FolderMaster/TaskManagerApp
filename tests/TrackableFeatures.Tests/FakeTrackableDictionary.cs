namespace TrackableFeatures.Tests
{
    public class FakeTrackableDictionary : TrackableDictionary<object, object>
    {
        private object? _lastEditedItem;

        public object? LastEditedItem
        {
            get => _lastEditedItem;
            set => UpdateProperty(ref _lastEditedItem, value);
        }

        public FakeTrackableDictionary(IDictionary<object, object>? dictionary = null) :
            base(dictionary) { }

        protected internal override void OnAddedItem(KeyValuePair<object, object> item,
            bool arePropertiesUpdate = true)
        {
            if (arePropertiesUpdate)
            {
                LastEditedItem = item;
            }
        }

        protected internal override void OnRemovedItem(KeyValuePair<object, object> item)
        {
            LastEditedItem = item;
        }
    }
}
