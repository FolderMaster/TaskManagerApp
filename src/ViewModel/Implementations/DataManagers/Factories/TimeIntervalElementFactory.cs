using Model.Interfaces;
using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.DataManagers.Factories
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
