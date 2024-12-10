using MachineLearning.Interfaces.Generals;

using TrackableFeatures;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Абстрактный класс базовой оценки модели обучения методом кросс-валидации.
    /// Наследует <see cref="TrackableObject"/>. Реализует <see cref="ILearningEvaluator"/>.
    /// </summary>
    public abstract class BaseCrossValidationLearningEvaluator : TrackableObject, ILearningEvaluator
    {
        /// <summary>
        /// Количество сегментов.
        /// </summary>
        private int _numberOfFolds = 2;

        /// <summary>
        /// Возвращает и задаёт количество сегментов.
        /// </summary>
        public int NumberOfFolds
        {
            get => _numberOfFolds;
            set => UpdateProperty(ref _numberOfFolds, value, OnPropertyChanged);
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="BaseCrossValidationLearningEvaluator"/>
        /// по умолчанию.
        /// </summary>
        public BaseCrossValidationLearningEvaluator()
        {
            OnPropertyChanged();
        }

        /// <summary>
        /// Вызывается при изменении свойства.
        /// </summary>
        protected abstract void OnPropertyChanged();
    }
}
