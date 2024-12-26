using System.Collections;

namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс списка временных интервалов.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ITimeInterval"/>, <see cref="IList{ITimeIntervalElement}"/> и
    /// <see cref="IList"/>.
    /// </remarks>
    public interface ITimeIntervalList : ITimeInterval, IList<ITimeIntervalElement>, IList { }
}
