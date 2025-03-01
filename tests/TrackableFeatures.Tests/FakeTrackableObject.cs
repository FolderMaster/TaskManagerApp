namespace TrackableFeatures.Tests
{
    public class FakeTrackableObject : TrackableObject
    {
        public static string NullOrEmptyPropertyError => $"{nameof(Property)} пусто!";

        public static string PropertyStartsWithTaskError =>
            $"{ nameof(Property)} начинается с Task!";

        public static string PropertyEndsWithManagerError =>
            $"{nameof(Property)} кончается на Manager!";

        private string _property;

        public string Property
        {
            get => _property;
            set => UpdateProperty(ref _property, value, OnPropertyChanged);
        }

        private void OnPropertyChanged(string oldValue, string newValue)
        {
            ClearAllErrors();
            if (string.IsNullOrEmpty(Property))
            {
                AddError(NullOrEmptyPropertyError, nameof(Property));
            }
            if (Property.StartsWith("Task"))
            {
                AddError(PropertyStartsWithTaskError, nameof(Property));
            }
            if (Property.EndsWith("Manager"))
            {
                AddError(PropertyEndsWithManagerError, nameof(Property));
            }
        }
    }
}
