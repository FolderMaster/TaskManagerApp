using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Implementations.ModelLearning.Converters;

namespace ViewModel.Implementations.ModelLearning
{
    /// <summary>
    /// Класс контроллера обучения модели обучения с учителем
    /// для предсказания запланированого реального показателя элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseSupervisedEvaluatorLearningController
    /// {IEnumerable{double}, double, ITaskElement, ITaskElement, double}"/>.
    /// </remarks>
    public class PlannedRealTaskElementEvaluatorLearningController :
        BaseSupervisedEvaluatorLearningController
        <IEnumerable<double>, double, ITaskElement, ITaskElement, double>
    {
        /// <summary>
        /// Создаёт экземпляр классса
        /// <see cref="PlannedRealTaskElementEvaluatorLearningController"/>.
        /// </summary>
        /// <param name="converter">
        /// Конвертор данных в данные для предсказания с учителем и наоборот.
        /// </param>
        /// <param name="model">Модель обучения c учителем.</param>
        /// <param name="evaluator">Оценка модели обучения с учителем.</param>
        public PlannedRealTaskElementEvaluatorLearningController
            (PlannedRealTaskElementLearningConverter converter,
            IRegressionModel model, IRegressionEvaluator evaluator) :
            base(converter, model, evaluator) { }
    }
}
