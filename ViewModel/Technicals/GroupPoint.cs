namespace ViewModel.Technicals
{
    public class GroupPoint : StatisticPoint
    {
        public int GroupIndex { get; private set; }

        public GroupPoint(double x, double y, string name, int groupIndex) :
            base(x, y, name)
        {
            GroupIndex = groupIndex;
        }
    }
}
