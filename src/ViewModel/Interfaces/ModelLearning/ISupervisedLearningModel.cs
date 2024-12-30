using MachineLearning.Interfaces.Generals;

namespace ViewModel.Interfaces.ModelLearning
{
    /// <summary>
    /// Интерфейс контроллера обучения модели обучения с учителем.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ILearningController{D, DT, DR}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    /// <typeparam name="D">Тип данных.</typeparam>
    /// <typeparam name="DT">Тип входных данных.</typeparam>
    /// <typeparam name="DR">Тип выходных данных.</typeparam>
    public interface ISupervisedLearningController<T, R, D, DT, DR> :
        ILearningController<D, DT, DR>
    {
        /// <summary>
        /// Возвращает и задаёт модель обучение с учителем.
        /// </summary>
        public ISupervisedLearningModel<T, R> LearningModel { get; set; }

        /// <summary>
        /// Возвращает и задаёт оцениватель модели обучения с учителем.
        /// </summary>
        public ISupervisedLearningEvaluator<T, R> LearningEvaluator { get; set; }
    }
}
