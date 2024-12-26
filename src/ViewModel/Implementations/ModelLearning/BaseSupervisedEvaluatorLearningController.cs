using MachineLearning;
using MachineLearning.Interfaces.Generals;

using ViewModel.Interfaces.ModelLearning;

namespace ViewModel.Implementations.ModelLearning
{
    public abstract class BaseSupervisedEvaluatorLearningController<T, R, D, DT, DR> :
        ILearningController<D, DT, DR>
    {
        private ISupervisedLearningConverter<T, R, D, DT, DR> _converter;

        private ISupervisedLearningModel<T, R> _model;

        private ISupervisedLearningEvaluator<T, R> _evaluator;

        public ScoreMetricCategory MinScoreCategory { get; set; }

        public bool IsValidModel { get; protected set; }

        public BaseSupervisedEvaluatorLearningController
            (ISupervisedLearningConverter<T, R, D, DT, DR> converter,
            ISupervisedLearningModel<T, R> model,
            ISupervisedLearningEvaluator<T, R> evaluator)
        {
            _converter = converter;
            _model = model;
            _evaluator = evaluator;
        }

        public async Task<bool> Train(IEnumerable<D> data)
        {
            if (data == null || !data.Any())
            {
                IsValidModel = false;
                return false;
            }
            var convertedLearningData = _converter.FitConvertData(data);
            _evaluator.Model = _model;
            var scoreCategory = await _evaluator.Evaluate
                (convertedLearningData.Data, convertedLearningData.Targets);
            if (scoreCategory >= MinScoreCategory)
            {
                await _model.Train
                    (convertedLearningData.Data, convertedLearningData.Targets);
                IsValidModel = true;
                return true;
            }
            IsValidModel = false;
            return false;
        }

        public DR Predict(DT data)
        {
            var convertedData = _converter.ConvertData(data);
            var predicted = _model.Predict(convertedData);
            var result = _converter.ConvertPredicted(predicted);
            return result;
        }
    }
}
