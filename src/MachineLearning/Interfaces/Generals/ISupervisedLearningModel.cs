namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс модели обучение c учителем.
    /// </summary>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public interface ISupervisedLearningModel<T, R> : ILearningModel<T, R>
    {
        /// <summary>
        /// Обучает модель на основе данных и целевых значений.
        /// </summary>
        /// <param name="data">Коллекция данных.</param>
        /// <param name="targets">Коллекция целевых значений.</param>
        /// <returns>Возвращает задачу процесса обучения.</returns>
        public Task Train(IEnumerable<IEnumerable<T>> data, IEnumerable<R> targets);
    }
}
