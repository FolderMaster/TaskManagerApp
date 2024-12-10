namespace MachineLearning
{
    /// <summary>
    /// Структура хранения данных для модели обучения.
    /// </summary>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public struct LearningModelData<T, R>
    {
        /// <summary>
        /// Возвращает и задаёт данные.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Возвращает и задаёт целевые значения.
        /// </summary>
        public IEnumerable<R> Target { get; set; }
    }
}
