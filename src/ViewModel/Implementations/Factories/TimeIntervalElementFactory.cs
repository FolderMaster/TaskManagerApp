using Model.Interfaces;

using ViewModel.Implementations.Sessions.Database.Domains;
using ViewModel.Interfaces;

namespace ViewModel.Implementations.Factories
{
    public class TimeIntervalElementFactory : IFactory<ITimeIntervalElement>
    {
        public ITimeIntervalElement Create() =>
            new TimeIntervalElementDomain(DateTime.Now, DateTime.Now)
            {
                Entity = new()
            };
    }
}
