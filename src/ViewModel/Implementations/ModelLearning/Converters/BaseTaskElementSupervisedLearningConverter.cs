using DynamicData;
using MachineLearning.Converters;
using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public abstract class BaseTaskElementSupervisedLearningConverter<R, DR> :
        BaseSupervisedLearningConverter<R, ITaskElement, ITaskElement, DR>
    {
        protected readonly IFactory<IScaler> _scalerFactory;

        protected readonly IDataTransformer<Metadata, int?> _metadataCategoriesTransformer;

        protected readonly IDataTransformer<Metadata, IEnumerable<int>> _metadataTagsTransformer;

        protected BaseTaskElementSupervisedLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory,
            IDataTransformer<Metadata, int?> metadataCategoriesTransformer,
            IDataTransformer<Metadata, IEnumerable<int>> metadataTagsITransformer) :
            base(primaryPointDataProcessor, pointDataProcessors)
        {
            _scalerFactory = scalerFactory;
            _metadataCategoriesTransformer = metadataCategoriesTransformer;
            _metadataTagsTransformer = metadataTagsITransformer;
        }

        protected override IEnumerable<IEnumerable<double?>> ProcessFeatures
            (IEnumerable<ITaskElement> data)
        {
            var metadataSet = data.Select(t => t.Metadata as Metadata);
            var categories = _metadataCategoriesTransformer.FitTransform(metadataSet);
            var tags = _metadataTagsTransformer.FitTransform(metadataSet);
            var index = 0;
            foreach (var dataItem in data)
            {
                var result = ExtractPrimaryFeatures(dataItem);
                result.Add(categories.ElementAt(index));
                result.AddRange(tags.ElementAt(index).Cast<double?>());
                yield return result;
                ++index;
            }
        }

        protected override IEnumerable<double?> ExtractFeatures(ITaskElement dataItem)
        {
            var metadata = dataItem.Metadata as Metadata;
            var result = ExtractPrimaryFeatures(dataItem);
            result.Add(_metadataCategoriesTransformer.Transform(metadata));
            result.AddRange(_metadataTagsTransformer.Transform(metadata).Cast<double?>());
            return result;
        }

        protected override IScaler CreateScaler
            (int index, IEnumerable<int> removedColumnsIndices) => _scalerFactory.Create();

        protected abstract List<double?> ExtractPrimaryFeatures(ITaskElement dataItem);
    }
}
