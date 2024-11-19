using Model.Interfaces;
using Model.Tasks.Times;

using ViewModel.Interfaces;

namespace ViewModel.Implementations.Factories
{
    public class TimeIntervalElementFactory : IFactory<ITimeIntervalElement>
    {
        public ITimeIntervalElement Create() =>
            new TimeIntervalElement(DateTime.Now, DateTime.Now);
    }
}
