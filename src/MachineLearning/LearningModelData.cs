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
        public IEnumerable<T> Data { get; private set; }

        /// <summary>
        /// Возвращает и задаёт целевые значения.
        /// </summary>
        public IEnumerable<R> Targets { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="LearningModelData{T, R}"/>.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="targets">целевые значения.</param>
        public LearningModelData(IEnumerable<T> data, IEnumerable<R> targets)
        {
            Data = data;
            Targets = targets;
        }
    }
}
