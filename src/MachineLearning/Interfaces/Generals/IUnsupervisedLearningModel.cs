namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс модели обучение без учителя. Наследует <see cref="ILearningModel{T, R}"/>.
    /// </summary>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public interface IUnsupervisedLearningModel<T, R> : ILearningModel<T, R>
    {
        /// <summary>
        /// Обучает модель на основе данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает задачу процесса обучения.</returns>
        public Task Train(IEnumerable<T> data);
    }
}
