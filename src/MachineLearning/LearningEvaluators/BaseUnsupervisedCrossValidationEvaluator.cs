using MachineLearning.Interfaces.Generals;

namespace MachineLearning.LearningEvaluators
{
    public abstract class BaseUnsupervisedCrossValidationEvaluator<T, R> :
        BaseCrossValidationLearningEvaluator, IUnsupervisedLearningEvaluator<T, R>
    {
        /// <summary>
        /// Модель обучения без учителя.
        /// </summary>
        private IUnsupervisedLearningModel<T, R> _model;

        /// <inheritdoc/>
        public IUnsupervisedLearningModel<T, R> Model
        {
            get => _model;
            set => UpdateProperty(ref _model, value, OnPropertyChanged);
        }

        /// <inheritdoc />
        public abstract Task<ScoreMetricCategory> Evaluate(IEnumerable<T> data);

        /// <inheritdoc />
        protected override void OnPropertyChanged<T>(T oldValue, T newValue)
        {
            base.OnPropertyChanged();
            if (Model == null)
            {
                AddError($"{nameof(Model)} должно быть назначено!");
            }
        }
    }
}
