using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Mappers
{
    public class TimeIntervalMapper : IMapper<TimeIntervalEntity, ITimeIntervalElement>
    {
        public ITimeIntervalElement Map(TimeIntervalEntity value) =>
            new TimeIntervalElementDomain(value.Start, value.End)
            {
                Entity = value
            };

        public TimeIntervalEntity MapBack(ITimeIntervalElement value)
        {
            if (value is not TimeIntervalElementDomain domain)
            {
                throw new ArgumentException(nameof(value));
            }
            var result = domain.Entity;
            result.Start = domain.Start;
            result.End = domain.End;
            return result;
        }
    }
}
