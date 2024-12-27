using MachineLearning.Interfaces.Generals;

using TrackableFeatures;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Абстрактный класс базовой оценки модели обучения методом кросс-валидации.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>. Реализует <see cref="ILearningEvaluator"/>.
    /// </remarks>
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
        protected virtual void OnPropertyChanged<T>(T oldValue, T newValue)
        {
            ClearAllErrors();
            if (NumberOfFolds <= 1)
            {
                AddError($"{nameof(NumberOfFolds)} должно быть больше 1!");
            }
        }
    }
}
