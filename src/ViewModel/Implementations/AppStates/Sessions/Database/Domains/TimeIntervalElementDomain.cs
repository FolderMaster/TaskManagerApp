using Model.Times;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Domains
{
    public class TimeIntervalElementDomain : TimeIntervalElement, IDomain
    {
        public TimeIntervalEntity Entity { get; set; }

        public object EntityId => Entity;

        public TimeIntervalElementDomain(DateTime? start = null, DateTime? end = null) :
            base(start, end)
        { }

        public TimeIntervalElementDomain() : this(null, null) { }
    }
}
