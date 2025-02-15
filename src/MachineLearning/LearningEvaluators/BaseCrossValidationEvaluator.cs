using MachineLearning.Aggregators;
using MachineLearning.Interfaces;
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
    public abstract class BaseCrossValidationLearningEvaluator :
        TrackableObject, ILearningEvaluator
    {
        /// <summary>
        /// Количество сегментов.
        /// </summary>
        private int _numberOfFolds = 2;

        /// <summary>
        /// Агрегатор.
        /// </summary>
        private IAggregator _aggregator = new MeanAggregator();

        /// <summary>
        /// Возвращает и задаёт количество сегментов.
        /// </summary>
        public int NumberOfFolds
        {
            get => _numberOfFolds;
            set => UpdateProperty(ref _numberOfFolds, value, OnPropertyChanged);
        }

        /// <summary>
        /// Возвращает агрегатор.
        /// </summary>
        public IAggregator Aggregator
        {
            get => _aggregator;
            set => UpdateProperty(ref _aggregator, value, OnPropertyChanged);
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
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <param name="oldValue">Старое значение.</param>
        /// <param name="newValue">Новое значение.</param>
        protected virtual void OnPropertyChanged<T>(T oldValue, T newValue)
        {
            ClearAllErrors();
            if (NumberOfFolds <= 1)
            {
                AddError($"{nameof(NumberOfFolds)} должно быть больше 1!");
            }
            if (Aggregator == null)
            {
                AddError($"{nameof(Aggregator)} должно быть назначено!");
            }
        }
    }
}
