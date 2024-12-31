using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Implementations.ModelLearning.Converters;

namespace ViewModel.Implementations.ModelLearning
{
    /// <summary>
    /// Класс контроллера обучения модели обучения с учителем
    /// для предсказания шанса выполнения элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseSupervisedEvaluatorLearningController
    /// {IEnumerable{double}, double, ITaskElement, ITaskElement, double}"/>.
    /// </remarks>
    public class ExecutionChanceTaskElementEvaluatorLearningController :
        BaseSupervisedEvaluatorLearningController
        <IEnumerable<double>, double, ITaskElement, ITaskElement, double>
    {
        /// <summary>
        /// Создаёт экземпляр классса
        /// <see cref="ExecutionChanceTaskElementEvaluatorLearningController"/>.
        /// </summary>
        /// <param name="converter">
        /// Конвертор данных в данные для предсказания с учителем и наоборот.
        /// </param>
        /// <param name="model">Модель обучения c учителем.</param>
        /// <param name="evaluator">Оценка модели обучения с учителем.</param>
        public ExecutionChanceTaskElementEvaluatorLearningController
            (ExecutionChanceTaskElementLearningConverter converter,
            IRegressionModel model, IRegressionEvaluator evaluator) :
            base(converter, model, evaluator) { }
    }
}
