using Database.Domains;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    /// <summary>
    /// Класс фабрики, создающая элементарные временные интервалы.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IFactory{ITimeIntervalElement}"/>.
    /// </remarks>
    public class TimeIntervalElementFactory : IFactory<ITimeIntervalElement>
    {
        /// <inheritdoc/>
        public ITimeIntervalElement Create() =>
            new TimeIntervalElementDomain()
            {
                Entity = new()
            };
    }
}
