using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Interfaces.DataManagers
{
    /// <summary>
    /// Интерфейс заместителя элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IProxy{ITaskElement}"/> и <see cref="ITaskElement"/>.
    /// </remarks>
    public interface ITaskElementProxy : IProxy<ITaskElement>, ITaskElement
    {
        /// <summary>
        /// Возвращает предсказанный срок.
        /// </summary>
        public DateTime? PredictedDeadline { get; }

        /// <summary>
        /// Возвращает предсказанное запланированное время.
        /// </summary>
        public TimeSpan PredictedPlannedTime { get; }

        /// <summary>
        /// Возвращает предсказанный реальный запланированный показатель.
        /// </summary>
        public double PredictedPlannedReal { get; }

        /// <summary>
        /// Возвращает логическое значение, указывающее на корректность предсказания срок.
        /// </summary>
        public bool IsValidPredictedDeadline { get; }

        /// <summary>
        /// Возвращает логическое значение, указывающее на корректность предсказания
        /// запланированного времени.
        /// </summary>
        public bool IsValidPredictedPlannedTime { get; }

        /// <summary>
        /// Возвращает логическое значение, указывающее на корректность предсказания
        /// реального запланированного показателя.
        /// </summary>
        public bool IsValidPredictedPlannedReal { get; }
    }
}
