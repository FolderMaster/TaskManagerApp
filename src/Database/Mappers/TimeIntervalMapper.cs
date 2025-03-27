using Model.Interfaces;

using Database.Domains;
using Database.Entities;

namespace Database.Mappers
{
    /// <summary>
    /// Класс перобразования значений временных интервалов между двумя предметными областями.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMapper{TimeIntervalEntity, ITimeIntervalElement}"/>.
    /// </remarks>
    public class TimeIntervalMapper : IMapper<TimeIntervalEntity, ITimeIntervalElement>
    {
        /// <inheritdoc/>
        public ITimeIntervalElement Map(TimeIntervalEntity value) =>
            new TimeIntervalElementDomain(value.Start, value.End)
            {
                Entity = value
            };

        /// <inheritdoc/>
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
