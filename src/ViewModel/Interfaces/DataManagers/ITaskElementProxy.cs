using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Interfaces.DataManagers
{
    public interface ITaskElementProxy : IProxy<ITaskElement>, ITaskElement
    {
        public DateTime? PredictedDeadline { get; }

        public TimeSpan PredictedPlannedTime { get; }

        public double PredictedPlannedReal { get; }

        public bool IsValidPredictedDeadline { get; }

        public bool IsValidPredictedPlannedTime { get; }

        public bool IsValidPredictedPlannedReal { get; }
    }
}
