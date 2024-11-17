namespace Model.Tasks.Ranges
{
    public class ProgressCompositeRangeValue : CompositeRangeValue<double>
    {
        public override double Value =>
            _items.Count > 0 ? _items.Sum(i => i.Value) / _items.Count : 0;

        public override double Min => 0;

        public override double Max => 100;

        protected override void UpdateProperties()
        {
            OnPropertyChanged(nameof(Value));
        }

        protected override void UpdateProperty(string? propertyName)
        {
            if (propertyName == nameof(Value))
            {
                OnPropertyChanged(nameof(Value));
            }
        }
    }
}
