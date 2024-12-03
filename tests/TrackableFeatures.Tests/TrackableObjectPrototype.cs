namespace TrackableFeatures.Tests
{
    public class TrackableObjectPrototype : TrackableObject
    {
        public static string NullOrEmptyPropertyError => $"{nameof(Property)} пусто!";

        public static string PropertyStartsWithTaskError =>
            $"{ nameof(Property)} начинается с Task!";

        public static string PropertyEndsWithManagerError =>
            $"{nameof(Property)} кончается с Manager!";

        private string _property;

        public string Property
        {
            get => _property;
            set => UpdateProperty(ref _property, value, OnPropertyChanged);
        }

        private void OnPropertyChanged()
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
