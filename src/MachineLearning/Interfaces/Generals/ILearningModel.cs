namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс модели обучение.
    /// </summary>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public interface ILearningModel<T, R>
    {
        /// <summary>
        /// Предсказывает значение на основе данных.
        /// </summary>
        /// <param name="data">Коллекция данных.</param>
        /// <returns>Возвращает предсказанное значение.</returns>
        public R Predict(IEnumerable<T> data);
    }
}
