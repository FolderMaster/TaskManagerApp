namespace ViewModel
{
    public class StatisticElement
    {
        public double Value { get; private set; }

        public string Name { get; private set; }

        public StatisticElement(double value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}
