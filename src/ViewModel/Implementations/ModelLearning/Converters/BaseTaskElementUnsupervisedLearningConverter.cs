using MachineLearning.Converters;
using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public abstract class BaseTaskElementUnsupervisedLearningConverter<R, DR> :
        BaseUnsupervisedLearningConverter<R, ITaskElement, ITaskElement, DR>
    {
        protected readonly IFactory<IScaler> _scalerFactory;

        protected readonly IDataTransformer<Metadata, int?> _metadataCategoriesTransformer;

        protected readonly IDataTransformer<Metadata, IEnumerable<int>> _metadataTagsITransformer;

        protected BaseTaskElementUnsupervisedLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory,
            IDataTransformer<Metadata, int?> metadataCategoriesTransformer,
            IDataTransformer<Metadata, IEnumerable<int>> metadataTagsITransformer) :
            base(primaryPointDataProcessor, pointDataProcessors)
        {
            _scalerFactory = scalerFactory;
            _metadataCategoriesTransformer = metadataCategoriesTransformer;
            _metadataTagsITransformer = metadataTagsITransformer;
        }

        protected override IScaler CreateScaler
            (int index, IEnumerable<int> removedColumnsIndices) => _scalerFactory.Create();
    }
}
