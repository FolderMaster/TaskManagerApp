using System.Collections;

namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс списка временных интервалов. Наследует <see cref="ITimeInterval"/>,
    /// <see cref="IList{ITimeIntervalElement}"/> и <see cref="IList"/>.
    /// </summary>
    public interface ITimeIntervalList : ITimeInterval, IList<ITimeIntervalElement>, IList { }
}
