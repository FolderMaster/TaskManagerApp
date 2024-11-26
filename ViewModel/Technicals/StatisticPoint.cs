namespace ViewModel.Technicals
{
    public class StatisticPoint : StatisticElement
    {
        public double Value2 { get; private set; }

        public StatisticPoint(double x, double y, string name) : base(x, name)
        {
            Value2 = y;
        }
    }
}
