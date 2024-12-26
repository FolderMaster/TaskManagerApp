using MachineLearning.Converters;
using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public abstract class BaseTaskElementSupervisedLearningConverter<R, DR> :
        BaseSupervisedLearningConverter<R, ITaskElement, ITaskElement, DR>
    {
        protected readonly IFactory<IScaler> _scalerFactory;

        protected BaseTaskElementSupervisedLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory) :
            base(primaryPointDataProcessor, pointDataProcessors)
        {
            _scalerFactory = scalerFactory;
        }

        protected override IScaler CreateScaler
            (int index, IEnumerable<int> removedColumnsIndices) => _scalerFactory.Create();
    }
}
