namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс модели обучение c учителем.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ILearningModel{T, R}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public interface ISupervisedLearningModel<T, R> : ILearningModel<T, R>
    {
        /// <summary>
        /// Обучает модель на основе данных и целевых значений.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="targets">Целевые значения.</param>
        /// <returns>Возвращает задачу процесса обучения.</returns>
        public Task Train(IEnumerable<T> data, IEnumerable<R> targets);
    }
}
