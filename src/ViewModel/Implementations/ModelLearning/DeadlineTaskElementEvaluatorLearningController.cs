using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Implementations.ModelLearning.Converters;

namespace ViewModel.Implementations.ModelLearning
{
    /// <summary>
    /// Класс контроллера обучения модели обучения с учителем
    /// для предсказания срока элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseSupervisedEvaluatorLearningController
    /// {IEnumerable{double}, double, ITaskElement, ITaskElement, DateTime?}"/>.
    /// </remarks>
    public class DeadlineTaskElementEvaluatorLearningController :
        BaseSupervisedEvaluatorLearningController
        <IEnumerable<double>, double, ITaskElement, ITaskElement, DateTime?>
    {
        /// <summary>
        /// Создаёт экземпляр классса <see cref="DeadlineTaskElementEvaluatorLearningController"/>.
        /// </summary>
        /// <param name="converter">
        /// Конвертор данных в данные для предсказания с учителем и наоборот.
        /// </param>
        /// <param name="model">Модель обучения c учителем.</param>
        /// <param name="evaluator">Оценка модели обучения с учителем.</param>
        public DeadlineTaskElementEvaluatorLearningController
            (DeadlineTaskElementLearningConverter converter,
            IRegressionModel model, IRegressionEvaluator evaluator) :
            base(converter, model, evaluator) { }
    }
}
