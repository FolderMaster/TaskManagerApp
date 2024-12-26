using MachineLearning;
using MachineLearning.Interfaces.Generals;

using ViewModel.Interfaces.ModelLearning;

namespace ViewModel.Implementations.ModelLearning
{
    public class BaseUnsupervisedEvaluatorLearningController<T, R, D, DT, DR> :
        ILearningController<D, DT, DR>
    {
        private IUnsupervisedLearningConverter<T, R, D, DT, DR> _converter;

        private IUnsupervisedLearningModel<T, R> _model;

        private IUnsupervisedLearningEvaluator<T, R> _evaluator;

        public ScoreMetricCategory MinScoreCategory { get; set; }

        public bool IsValidModel { get; private set; }

        public BaseUnsupervisedEvaluatorLearningController
            (IUnsupervisedLearningConverter <T, R, D, DT, DR> converter,
            IUnsupervisedLearningModel<T, R> model,
            IUnsupervisedLearningEvaluator<T, R> evaluator)
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
            var convertedData = _converter.FitConvertData(data);
            _evaluator.Model = _model;
            var scoreCategory = await _evaluator.Evaluate(convertedData);
            if (scoreCategory >= MinScoreCategory)
            {
                await _model.Train(convertedData);
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
